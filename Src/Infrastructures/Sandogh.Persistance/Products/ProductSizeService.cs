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
    public class ProductSizeService : EfRepository<ProductSize>, IProductSize
    {
        public ProductSizeService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
