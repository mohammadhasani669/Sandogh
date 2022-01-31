using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sandogh.Admin.EndPoint.Models;
using Sandogh.Application.Visitors.GetToDayReport;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IAdminMenu _menu;
        
        private readonly IGetTodayReportService _getTodayReportService;
        public ResultTodayReportDto ResultTodayReport;

        public HomeController(ILogger<HomeController> logger, IGetTodayReportService getTodayReportService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAdminMenu menu)
        {
            _logger = logger;
            _getTodayReportService = getTodayReportService;
            _userManager = userManager;
            _roleManager = roleManager;
            _menu = menu;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            //InsertAdminUser();
            //ResultTodayReport = _getTodayReportService.Execute();
            // return View(ResultTodayReport);
            return View();
        }

        private void InsertAdminUser()
        {
            var AdminExist = _userManager.Users.Any(x => x.Email.ToUpper() == "MOHAMMADHASANI669@GMAIL.COM");

            if (!AdminExist)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "mohammadhasani669@gmail.com",
                    Email = "mohammadhasani669@gmail.com",
                    FirstName = "mohammaad",
                    LastName = "Hasani",
                    PhoneNumber = "09026781836",
                    FullName = "mohammad hasani",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };

                var result = _userManager.CreateAsync(adminUser, "12345678").Result;
                if (result.Succeeded)
                {
                    var RoleAdminExist = _roleManager.Roles.Any(x => x.Name == "Admin");
                    if (!RoleAdminExist)
                    {
                        var roleAdmin = new IdentityRole { Name = "Admin" };
                        var createRoleResult = _roleManager.CreateAsync(roleAdmin).Result;
                        var AddToRoleResult = _userManager.AddToRoleAsync(adminUser, "Admin").Result;
                    }
                }
            }
        }

        private void InsertMenu()
        {
            AdminMenu menu = new AdminMenu 
            { 
                Name = "Level01",
                SubMenu = new List<AdminMenu> { new AdminMenu { Name = "Level02" } }
            };
            _menu.Insert(menu);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
