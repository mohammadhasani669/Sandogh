using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Carts
{
    public class Cart : Entity
    {
        public string BuyerId { get; private set; }
        private readonly List<CartItem> _items = new List<CartItem>();

        public ICollection<CartItem> Items => _items.AsReadOnly();
        public Cart()
        {

        }
        public Cart(string buyerId)
        {
            this.BuyerId = buyerId;
        }

        public void AddItem(int productId, int quantity, int unitPrice)
        {
            if (!Items.Any(p => p.ProductId == productId))
            {
                _items.Add(new CartItem(productId, quantity, unitPrice));
                return;
            }
            var existingItem = Items.FirstOrDefault(p => p.ProductId == productId);
            existingItem.AddQuantity(quantity);
        }
    }
}
