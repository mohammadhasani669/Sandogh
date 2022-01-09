using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Visitors.GetToDayReport
{
    public interface IGetTodayReportService
    {
        ResultTodayReportDto Execute();
    }
}
