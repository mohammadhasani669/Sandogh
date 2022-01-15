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
        public string Description { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public bool Enable { get; set; }
        public bool HasSize { get; set; }
        public bool HasColor { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory productCategory { get; set; }

        public int? SizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductFeature>  ProductFeatures { get; set; }
     
    }
}
