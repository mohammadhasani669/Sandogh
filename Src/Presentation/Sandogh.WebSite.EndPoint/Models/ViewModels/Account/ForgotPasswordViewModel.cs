using System.ComponentModel.DataAnnotations;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="لطفا ایمیل را وارد کنید")]
        //[EmailAddress(ErrorMessage ="لطفا ایمیل معتبر وارد نمایید")]
        [DataType(DataType.EmailAddress,ErrorMessage = "لطفا ایمیل معتبر وارد نمایید")]
        public string Email { get; set; }
    }
}
