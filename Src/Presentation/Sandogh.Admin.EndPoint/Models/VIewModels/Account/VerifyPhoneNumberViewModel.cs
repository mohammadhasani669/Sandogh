using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Account
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
