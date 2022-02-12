using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sandogh.Admin.EndPoint.Attributes;
using Sandogh.Admin.EndPoint.Models.VIewModels.Roles;
using Sandogh.Admin.EndPoint.Security.IdentityService;
using Sandogh.Domain.Users;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUtilities _utilities;
        private readonly IMemoryCache _memoryCache;

        public RoleController(RoleManager<IdentityRole> roleManager, IUtilities utilities, IMemoryCache memoryCache, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _utilities = utilities;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Edit(string id)
        {
            var model = new EditRoleModel();
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "نقش یافت نشد!");
                return View(model);
            }
            model.Id = role.Id;
            model.RoleName = role.Name;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "خطا در ویرایش");
                return View(model);
            }
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "نقش یافت نشد!");
                return View(model);
            }
            role.Name = model.RoleName;

            var result = _roleManager.UpdateAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            UpdateSecurityStamps(model.Id);
            ModelState.AddModelError(string.Empty, "خطا در ویرایش");
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Role Created";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            var role = _roleManager.Roles.SingleOrDefault(x => x.Id == Id);
            if (role != null)
            {
                try
                {
                    var result = _roleManager.DeleteAsync(role).Result;
                }
                catch (Exception)
                {
                    TempData["Error"] = "خطا در حذف ";
                    RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateOrEditRoleClaim(string id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return View();

            var roleClaims = _roleManager.GetClaimsAsync(role).Result.ToList();

            var allMvcNames =
              _memoryCache.GetOrCreate("AreaAndActionAndControllerNamesList", p =>
              {
                  p.AbsoluteExpiration = DateTimeOffset.MaxValue;
                  return _utilities.AreaAndActionAndControllerNamesList();
              });

            allMvcNames = allMvcNames
             .GroupBy(x => new { x.ControllerName, x.ActionName, x.Description })
             .Select(g => new ActionAndControllerName
             {
                 ActionName = g.Key.ActionName,
                 ControllerName = g.Key.ControllerName,
                 Description = g.Key.Description
             }).ToList();

            var model = new CreateOrEditRoleCalimViewModel()
            {
                RoleId = id,
                RoleName = role.Name,
                ActionAndControllerNames = allMvcNames,
                RoleCailms = roleClaims
            };

            return View(model);
        }

        [HttpPost]
        [PermissionName(Title = "ساخت")]
        public async Task<IActionResult> CreateOrEditRoleClaim(CreateOrEditRoleCalimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(model.RoleId).Result;

                if (role != null)
                {
                    var requestRoles =
                        model.ActionAndControllerNames.Where(c => c.IsSelected).ToList();

                    var roleClaims = _roleManager.GetClaimsAsync(role).Result.ToList();
                    foreach (var claim in roleClaims)
                    {
                        await _roleManager.RemoveClaimAsync(role, claim);
                    }
                    foreach (var requestRole in requestRoles)
                    {

                        await _roleManager.AddClaimAsync(role,
                            new Claim($"{requestRole.ControllerName}|{requestRole.ActionName}".ToUpper(),
                                true.ToString()));
                    }
                    UpdateSecurityStamps(model.RoleId);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        private void UpdateSecurityStamps(string roleId)
        {
            var RoleName = _roleManager.FindByIdAsync(roleId).Result;
            var users = _userManager.GetUsersInRoleAsync(RoleName.Name).Result;
            foreach (var user in users)
            {
                _userManager.UpdateSecurityStampAsync(user).Wait();
            }
        }

    }
}
