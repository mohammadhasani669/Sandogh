using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Visitors.SaveVisitorInfo
{
    internal interface ISaveVisitorInfoService
    {
        void Execute(RequestSaveVisitorInfoDto request);
    }
}
