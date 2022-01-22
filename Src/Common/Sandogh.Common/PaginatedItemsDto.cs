﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Common
{
    public class PaginatedItemsDto<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedItemsDto(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
