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

        public CartDto GetBasketForUser(string UserId)
        {
            var basket = _context.Carts
            .Include(p => p.Items)
            .ThenInclude(p => p.Product)
            .ThenInclude(p => p.Images)
            .FirstOrDefault(p => p.BuyerId == UserId);
            if (basket == null)
            {
                return null;
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

        public CartDto GetOrCreateBasketForUser(string BuyerId)
        {
            var basket = _context.Carts
              .Include(p => p.Items)
              .ThenInclude(p => p.Product)
              .ThenInclude(p => p.Images)
              .FirstOrDefault(p => p.BuyerId == BuyerId);
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

        public bool RemoveItemFromCart(int ItemId)
        {
            var item = _context.CartItems.SingleOrDefault(x => x.Id == ItemId);
            _context.CartItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool SetQuantities(int itemId, int quantity)
        {
            var item = _context.CartItems.SingleOrDefault(p => p.Id == itemId);
            item.SetQuantity(quantity);
            _context.SaveChanges();
            return true;
        }

        public void TransferBasket(string anonymousId, string UserId)
        {
            var anonymousBasket = _context.Carts
                .Include(x => x.Items)
                .SingleOrDefault(p => p.BuyerId == anonymousId);
            if (anonymousBasket == null) return;
            var userBasket = _context.Carts.SingleOrDefault(p => p.BuyerId == UserId);
            if (userBasket == null)
            {
                userBasket = new Cart(UserId);
                _context.Carts.Add(userBasket);
            }

            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            
            _context.Carts.Remove(anonymousBasket);
            _context.SaveChanges();
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
