using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Users
{
    public class EditUserRolesViewModel
    {
        public string UserName { get; set; }

        public string UserId { get; set; }
        public List<string> UserRoles { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
