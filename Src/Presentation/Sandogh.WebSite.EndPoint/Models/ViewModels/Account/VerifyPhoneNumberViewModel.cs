using System.ComponentModel.DataAnnotations;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class VerifyPhoneNumberViewModel
    {
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Code { get; set; }
    }
}
