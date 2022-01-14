using Sandogh.Domain.Emails;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Sandogh.Application.Emails
{
    public class EmailService : EfRepository<Email>, IEmailService
    {
       

        public EmailService(DatabaseContext dbcontext):base(dbcontext)
        {
           
        }

    

        public void Send(Email email, string password)
        {
            //enable less secure apps in account google with link
            //https://myaccount.google.com/lesssecureapps

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            //در خط بعدی ایمیل  خود و پسورد ایمیل خود  را جایگزین کنید
            client.Credentials = new NetworkCredential("DavidJohnsonub313@gmail.com", password);
            MailMessage message = new MailMessage("DavidJohnsonub313@gmail.com", email.To, email.Subject, email.Body);
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);
        }
    }
}
