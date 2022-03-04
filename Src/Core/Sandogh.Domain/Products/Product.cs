using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public bool Enable { get; set; }
        public bool HasSize { get; set; }
        public bool HasColor { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int? SizeId { get; set; }
        public Size Size { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductFeature>  Features { get; set; }
     
    }
}
