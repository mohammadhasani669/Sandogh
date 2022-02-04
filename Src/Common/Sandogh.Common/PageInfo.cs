using System;
using System.Collections.Generic;

namespace Sandogh.Common
{
    public class PageInfo
    {
        public int PageSize { get; set; }
        public long TotalCount { get; set; }

        public int PageNumber { get; set; }

        //public int PageCount => (int)Math.Ceiling((double)(TotalCount / PageSize));
        public IEnumerable<int> PageCount { get; set; }
        public Pager Pager { get; private set; }
    }

    public class PagedData<T>
    {
        public IEnumerable<T> Data { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
