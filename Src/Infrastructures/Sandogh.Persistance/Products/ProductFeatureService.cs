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
    public class ProductFeatureService : EfRepository<ProductFeature>, IProductFeature
    {
        public ProductFeatureService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
