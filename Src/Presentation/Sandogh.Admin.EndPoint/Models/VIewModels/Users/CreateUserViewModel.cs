using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Users
{
    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "الزامیست")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "الزامیست")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "الزامیست")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "الزامیست")]
        public string LastName { get; set; }
        [Display(Name = "موبایل")]
        [RegularExpression(@"(\+98|0)?9\d{9}", ErrorMessage = "موبایل معتبر نیست")]
        public string PhoneNumber { get; set; }

    }
}
