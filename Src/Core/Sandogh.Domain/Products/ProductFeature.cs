using Sandogh.Domain.Common;

namespace Sandogh.Domain.Products
{
    public class ProductFeature : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
