using Sandogh.Domain.Common;
using System.Collections.Generic;

namespace Sandogh.Domain.Products
{
    public class Size : Entity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
