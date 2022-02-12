using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "موبایل")]
        [RegularExpression(@"(\+98|0)?9\d{9}", ErrorMessage = "موبایل معتبر نیست")]
        public string PhoneNumber { get; set; }
    }
}
