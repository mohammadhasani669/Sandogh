using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Common
{
    public class PageInfo
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageCount => (int)Math.Ceiling((double)(TotalCount / PageSize));
    }

    public class PagedData<T>
    {
        public IEnumerable<T> Data { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
