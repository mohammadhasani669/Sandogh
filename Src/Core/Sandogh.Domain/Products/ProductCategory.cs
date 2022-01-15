using Sandogh.Domain.Common;
using System.Collections.Generic;

namespace Sandogh.Domain.Products
{
    public class ProductCategory : Entity
    {
        public string Name { get; set; }
        public bool Enable { get; set; }
        public string Image { get; set; }
        public string ImageTitle { get; set; }
        public int? ParentId { get; set; }
        public ProductCategory ParentProductCategory { get; set; }
        public ICollection<ProductCategory> SubProductCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
