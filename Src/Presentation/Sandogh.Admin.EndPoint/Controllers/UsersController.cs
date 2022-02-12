using CharismaWebSite.Core.Domain.Users;
using CharismaWebSite.EndPoints.Admin.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Admin.EndPoint.Models.VIewModels.Account;
using Sandogh.Admin.EndPoint.Models.VIewModels.Users;
using Sandogh.Common;
using Sandogh.Domain.Users;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var model = _userManager.Users.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا در ثبت کاربر");
                return View(model);
            }

            ApplicationUser user = new ApplicationUser();

            user.UserName = model.Email?.PersianToEnglish();
            user.Email = model.Email?.PersianToEnglish();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber.PersianToEnglish();

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(string id)
        {

            var user = _userManager.FindByIdAsync(id).Result;
            var editUser = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا در ویرایش کاربر");
                return View(model);
            }

            var user = _userManager.FindByIdAsync(model.Id).Result;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber?.PersianToEnglish();

            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> EditUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();

            var model = new EditUserRolesViewModel
            {
                UserName = user.UserName,
                UserId = user.Id,
                Roles = roles,
                UserRoles = userRoles.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var curentRole = await _userManager.GetRolesAsync(user);

            foreach (var role in curentRole)
            {
                if (!roles.Any(c => c == role))
                {
                    var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            foreach (var role in roles)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role);
                if (!isInRole)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
                }
            }

            return RedirectToAction("Index", "Users");
        }

        public IActionResult ResetPassword(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            return View(new ResetPasswordViewModel
            {
                Token = token,
                UserId = id,
                UserName = user.UserName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);
            if (reset.Password != reset.ConfirmPassword)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(reset.UserId).Result;
            if (user == null)
            {
                return BadRequest();
            }

            var Result = _userManager.ResetPasswordAsync(user, reset.Token, reset.Password).Result;

            if (Result.Succeeded)
            {
                TempData["msg"] = "رمز عبور با موفقیت تغییر کرد";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(reset);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                TempData["msg"] = "خطا در حذف";
                return RedirectToAction(nameof(Index));
            }

            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
