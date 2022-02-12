using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Roles
{
    public class CreateOrEditRoleCalimViewModel
    {
        public CreateOrEditRoleCalimViewModel()
        {
            ActionAndControllerNames = new List<ActionAndControllerName>();
        }

        public string RoleId { get; set; }

      
        [Display(Name = "نام مقام")]
        public string RoleName { get; set; }
        public IList<ActionAndControllerName> ActionAndControllerNames { get; set; }
        public IList<Claim>? RoleCailms { get; set; }
    }

    public class ActionAndControllerName
    {
        public string AreaName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
        public string Claim => $"{ControllerName}|{ActionName}".ToUpper();
        public bool IsSelected { get; set; }
    }
}
