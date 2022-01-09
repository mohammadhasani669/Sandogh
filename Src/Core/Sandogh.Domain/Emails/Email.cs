using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Emails
{
    public class Email : Entity
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
    }
}
