using MediatR;
using Microsoft.EntityFrameworkCore;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Common;
using Sandogh.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Products.Queries.GetAll
{
    public class GetAllProductQueryHandler : RequestHandler<GetAllProductQuery, PaginatedItemsDto<GetAllProductQuery>>
    {
        private readonly IProduct _product;
        private readonly IDataBaseContext  _context;

        public GetAllProductQueryHandler(IProduct product, IDataBaseContext context)
        {
            _product = product;
            _context = context;
        }

        protected override PaginatedItemsDto<GetAllProductQuery> Handle(GetAllProductQuery request)
        {
            int rowCount = 0;
            var data = _context.Products
                .Include(p => p.Images)
                .OrderByDescending(p => p.Id)
                .PagedResult(request.page, request.pageSize, out rowCount)
                .Select(p => new GetAllProductQuery
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Images.FirstOrDefault().Src,
                }).ToList();
            return new PaginatedItemsDto<GetAllProductQuery>(request.page, request.pageSize, rowCount, data);
        }
    }
}
