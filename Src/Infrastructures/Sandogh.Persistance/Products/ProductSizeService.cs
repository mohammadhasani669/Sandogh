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
    public class ProductSizeService : EfRepository<Size>, ISize
    {
        private readonly DatabaseContext _context;
        public ProductSizeService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public IEnumerable<Size> GetAllForDDL()
        {
            return _context.Sizes.Select(x => new Size { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
