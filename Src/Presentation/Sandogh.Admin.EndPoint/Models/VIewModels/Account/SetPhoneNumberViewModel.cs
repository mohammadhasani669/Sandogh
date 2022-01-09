using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Account
{
    public class SetPhoneNumberViewModel
    {
        [Required]
        [RegularExpression(@"(\+98|0)?9\d{9}")]
        public string PhoneNumber { get; set; }
    }
}
