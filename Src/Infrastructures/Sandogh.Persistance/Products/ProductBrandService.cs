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
        public ProductBrandService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
