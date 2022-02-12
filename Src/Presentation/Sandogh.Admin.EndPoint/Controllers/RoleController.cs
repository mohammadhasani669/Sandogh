using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sandogh.Admin.EndPoint.Attributes;
using Sandogh.Admin.EndPoint.Models.VIewModels.Roles;
using Sandogh.Admin.EndPoint.Security.IdentityService;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUtilities _utilities;
        private readonly IMemoryCache _memoryCache;

        public RoleController(RoleManager<IdentityRole> roleManager, IUtilities utilities, IMemoryCache memoryCache)
        {
            _roleManager = roleManager;
            _utilities = utilities;
            _memoryCache = memoryCache;
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
        public IActionResult CreateOrEditRoleClaim()
        {
            var allMvcNames =
                _memoryCache.GetOrCreate("AreaAndActionAndControllerNamesList", p =>
                {
                    p.AbsoluteExpiration = DateTimeOffset.MaxValue;
                    return _utilities.AreaAndActionAndControllerNamesList();
                });
            var model = new CreateOrEditRoleCalimViewModel()
            {
                ActionAndControllerNames = allMvcNames
            };

            return View(model);
        }

        [HttpPost]
        [PermissionName(Title = "ساخت")]
        public async Task<IActionResult> CreateOrEditRoleClaim(CreateOrEditRoleCalimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(model.RoleName);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    var requestRoles =
                        model.ActionAndControllerNames.Where(c => c.IsSelected).ToList();
                    foreach (var requestRole in requestRoles)
                    {
                        var areaName = (string.IsNullOrEmpty(requestRole.AreaName)) ?
                            "NoArea" : requestRole.AreaName;

                        await _roleManager.AddClaimAsync(role,
                            new Claim($"{areaName}|{requestRole.ControllerName}|{requestRole.ActionName}".ToUpper(),
                                true.ToString()));
                    }


                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

    }
}
