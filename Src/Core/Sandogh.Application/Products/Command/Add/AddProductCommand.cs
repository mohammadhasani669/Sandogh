using MediatR;
using Sandogh.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandogh.Application.Products.Command.Add
{
    public class AddProductCommand : IRequest<int>
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
        public int? SizeId { get; set; }
        public int BrandId { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductFeature> Features { get; set; }
    }
}
