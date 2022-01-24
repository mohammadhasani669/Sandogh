using Sandogh.Domain.Common;
using Sandogh.Domain.Products;

namespace Sandogh.Domain.Carts
{
    public class CartItem : Entity
    {
        public int UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int CartId { get; private set; }
        public CartItem(int productId, int quantity, int unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
