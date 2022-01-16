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
    public class ProductBrandService : EfRepository<ProductBrand>, IProductBrand
    {
        private readonly DatabaseContext _context;
        public ProductBrandService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public IEnumerable<ProductBrand> GetAllForDDL()
        {
            return _context.ProductBrands.Select(x => new ProductBrand{ Id = x.Id, Name= x.Name }).ToList();
        }
    }
}
