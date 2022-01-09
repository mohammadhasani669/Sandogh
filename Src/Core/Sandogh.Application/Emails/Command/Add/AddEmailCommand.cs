using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Emails.Command.Add
{
    public class AddEmailCommand:IRequest<int>
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
    }
}
