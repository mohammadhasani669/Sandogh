using Sandogh.Domain.Common;

namespace Sandogh.Domain.Products
{
    public class ProductImage : Entity
    {
        public string Src { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
