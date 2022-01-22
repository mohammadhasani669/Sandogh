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

namespace Sandogh.Application.Products.Queries.PDP
{
  

    public class PDPQueryHandler : RequestHandler<PDPQuery,PDPQuery>
    {
        private readonly IDataBaseContext _context;

        public PDPQueryHandler(IDataBaseContext context)
        {
            _context = context;
        }

        protected override PDPQuery Handle(PDPQuery request)
        {
            var product = _context.Products
          .Include(p => p.Features)
          .Include(p => p.Images)
          .Include(p => p.Category)
          .Include(p => p.Brand)
          .FirstOrDefault(p => p.Id == request.Id);

            var feature = product.Features
                .Select(p => new ProductFeature
                {
                    Group = p.Group,
                    Key = p.Key,
                    Value = p.Value
                }).ToList()
                .GroupBy(p => p.Group);

            var similarProducts = _context
               .Products
               .Include(p => p.Images)
               .Where(p => p.ProductCategoryId == product.ProductCategoryId)
               .Take(10)
               .Select(p => new SimilarProducts
               {
                   Id = p.Id,
                   Images = p.Images.FirstOrDefault().Src,
                   Price = p.Price,
                   Name = p.Name
               }).ToList();

            var data = new PDPQuery();

            data.Features = feature;
            data.SimilarCatalogs = similarProducts;
            data.Id = product.Id;
            data.Name = product.Name;
            data.Brand = product.Brand.Name;
            data.Type = product.Category.Name;
            data.Price = product.Price;
            data.Description = product.Description;
            data.Images = product.Images.Select(p => p.Src).ToList();
            
            return data;
        }
    }
}
