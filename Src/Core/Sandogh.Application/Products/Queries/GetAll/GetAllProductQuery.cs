using MediatR;
using Sandogh.Common;
using Sandogh.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Products.Queries.GetAll
{
    public class GetAllProductQuery : IRequest<PaginatedItemsDto<GetAllProductQuery>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public byte Rate { get; set; }
    }
}
