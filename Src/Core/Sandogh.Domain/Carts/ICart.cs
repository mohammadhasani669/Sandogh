using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Carts
{
    public interface ICart : IRepository<Cart>
    {
        CartDto GetOrCreateBasketForUser(string BuyerId);
        void AddItemToCart(int cartId, int productId, int qty = 1);
        bool RemoveItemFromCart(int ItemId);
        bool SetQuantities(int itemId, int quantity);
        CartDto GetBasketForUser(string UserId);
        void TransferBasket(string anonymousId, string UserId);
    }
}
