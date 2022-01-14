using Sandogh.Domain.Common;
using Sandogh.Domain.Emails;

namespace Sandogh.Application.Emails
{
    public interface IEmailService:IRepository<Email>
    {
        void Send(Email email,string password);
       
    }
}
