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
    public class ProductBrandService : EfRepository<Brand>, IBrand
    {
        private readonly DatabaseContext _context;
        public ProductBrandService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public IEnumerable<Brand> GetAllForDDL()
        {
            return _context.Brands.Select(x => new Brand{ Id = x.Id, Name= x.Name }).ToList();
        }
    }
}
