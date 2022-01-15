using Sandogh.Domain.Common;
using System.Collections.Generic;

namespace Sandogh.Domain.Products
{
    public class ProductBrand : Entity
    {
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageTitle { get; set; }
        public ICollection<Product> Products { get; set; }
        public bool Enable { get; set; }
    }
}
