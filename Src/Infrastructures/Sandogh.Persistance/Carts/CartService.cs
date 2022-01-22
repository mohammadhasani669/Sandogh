using Microsoft.EntityFrameworkCore;
using Sandogh.Domain.Carts;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Carts
{
    public class CartService : EfRepository<Cart>, ICart
    {
        private readonly DatabaseContext _context;
        public CartService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public void AddItemToCart(int cartId, int productId, int qty = 1)
        {
            var cart = _context.Carts.SingleOrDefault(x => x.Id == cartId);
            if (cart == null)
                throw new Exception();
            var product = _context.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null) 
                throw new Exception();
            cart.AddItem(productId, qty, product.Price);
            _context.SaveChanges();
        }

        public CartDto GetOrCreateBasketForUser(string BuyerId)
        {
            var basket = _context.Carts
              .Include(p => p.Items)
              .ThenInclude(p => p.Product)
              .ThenInclude(p => p.Images)
              .SingleOrDefault(p => p.BuyerId == BuyerId);
            if (basket == null)
            {
                //سبد خرید را ایجاد کنید
                return CreateBasketForUser(BuyerId);
            }
            return new CartDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    Id = item.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ImageUrl = item?.Product?.Images?.FirstOrDefault()?.Src ?? "",

                }).ToList(),
            };
        }
        private CartDto CreateBasketForUser(string BuyerId)
        {
            Cart basket = new Cart(BuyerId);
            _context.Carts.Add(basket);
            _context.SaveChanges();
            return new CartDto
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
            };
        }
    }


}
