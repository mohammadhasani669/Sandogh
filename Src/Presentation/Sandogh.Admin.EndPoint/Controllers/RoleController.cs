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
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            var allMvcNames =
                _memoryCache.GetOrCreate("AreaAndActionAndControllerNamesList", p =>
                {
                    p.AbsoluteExpiration = DateTimeOffset.MaxValue;
                    return _utilities.AreaAndActionAndControllerNamesList();
                });
            var model = new CreateRoleViewModel()
            {
                ActionAndControllerNames = allMvcNames
            };

            return View(model);
        }

        [HttpPost]
        [PermissionName(Title = "ساخت")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
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
