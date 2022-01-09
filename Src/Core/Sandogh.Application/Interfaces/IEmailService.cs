using Sandogh.Application.Interfaces;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Emails
{
    public interface IEmailService:IRepository<Email>
    {
        void Send(Email email,string password);
       
    }

    public class EmailService : EfRepository<Email>, IEmailService
    {
       

        public EmailService(IDataBaseContext dbcontext):base(dbcontext)
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
