using System.ComponentModel.DataAnnotations;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class SetPhoneNumberViewModel
    {
        [Required]
        [RegularExpression(@"(\+98|0)?9\d{9}")]
        public string PhoneNumber { get; set; }
    }
}
