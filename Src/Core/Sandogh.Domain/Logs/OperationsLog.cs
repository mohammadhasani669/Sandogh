using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Logs
{
    public class OperationsLog : Entity
    {
       
        public string ActionName { get; set; }
        public string Entity { get; set; }
        public int RecordId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
        public string IP { get; set; }
        public DateTime DT { get; set; }
        public string SerializedData { get; set; }
    }
}
