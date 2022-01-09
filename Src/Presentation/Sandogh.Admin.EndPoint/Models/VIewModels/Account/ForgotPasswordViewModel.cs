using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
