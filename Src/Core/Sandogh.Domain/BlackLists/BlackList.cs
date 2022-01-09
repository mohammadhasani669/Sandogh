using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.BlackLists
{
    public class BlackList : Entity
    {
      
        public string InputText { get; set; }
        public DateTime InsertDate { get; set; }
        public string IP { get; set; }
        public string AdditionalInformation { get; set; }

    }
}
