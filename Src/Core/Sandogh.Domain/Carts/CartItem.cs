using Sandogh.Domain.Products;

namespace Sandogh.Domain.Carts
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int BasketId { get; private set; }
        public CartItem(int productId, int quantity, int unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
