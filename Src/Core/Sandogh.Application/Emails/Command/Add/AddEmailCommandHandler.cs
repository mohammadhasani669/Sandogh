using MediatR;
using Sandogh.Domain.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Emails.Command.Add
{
    public class AddEmailCommandHandler : RequestHandler<AddEmailCommand, int>
    {
        private readonly IEmailService _emailService;

        public AddEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        protected override int Handle(AddEmailCommand request)
        {
            var email = new Email
            {
                Subject = request.Subject,
                Body = request.Body,
                Code = request.Code,
                To = request.To,
            };
            _emailService.Insert(email);
            return email.Id;
        }
    }
}
