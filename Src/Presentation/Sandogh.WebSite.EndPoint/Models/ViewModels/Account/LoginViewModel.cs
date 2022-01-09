using System.ComponentModel.DataAnnotations;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="ایمیل را وارد نمایید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage ="رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
