using Sandogh.Domain.Products;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Products
{
    public class ProductCategoryService : EfRepository<ProductCategory>, IProductCategory
    {
        private readonly DatabaseContext _context;
        public ProductCategoryService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public IEnumerable<ProductCategory> GetAllForDDL()
        {
            return _context.ProductCategories.Select(c => new ProductCategory {Id = c.Id , Name = c.Name }).ToList();
        }
    }
}
