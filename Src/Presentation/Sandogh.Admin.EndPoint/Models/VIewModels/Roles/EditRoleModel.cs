using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Roles
{
    public class EditRoleModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "نام نقش")]
        public string RoleName { get; set; }
    }
}
