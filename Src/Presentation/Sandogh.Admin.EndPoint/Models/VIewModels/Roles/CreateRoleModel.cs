using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Roles
{
    public class CreateRoleModel
    {
        [Required]
        [Display(Name = "نام نقش")]
        public string RoleName { get; set; }
    }
}
