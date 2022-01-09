using System.ComponentModel.DataAnnotations;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name ="نام و نام خانوادگی")]
        [Required(ErrorMessage ="نام و نام خانوادگی را ولرد نمایید")]
        [MaxLength(100,ErrorMessage ="نام و نام خانوادگی نباید بیش تز 100 کاراکتر باشد")]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="ایمیل را وارد نمایید")]
        public string Email { get; set; }

        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage ="رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "تکرار رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="رمز عبور با تکرار آن باید برابر باشد")]
        public string RePassword { get; set; }

        [Display(Name ="موبایل")]
        public string PhoneNumber  { get; set; }
    }
}
