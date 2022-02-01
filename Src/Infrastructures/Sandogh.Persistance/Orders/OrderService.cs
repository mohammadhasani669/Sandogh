using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sandogh.Domain.Orders;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Orders
{
   
    public class OrderService : EfRepository<Order>,IOrder
    {

        private readonly DatabaseContext _context;
        private readonly IMapper mapper;

        public OrderService(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            this.mapper = mapper;
        }

        public int CreateOrder(int cartId, int UserAddressId, PaymentMethod paymentMethod)
        {
            var cart = _context.Carts
                         .Include(p => p.Items)
                         .SingleOrDefault(p => p.Id == cartId);

            int[] Ids = cart.Items.Select(p => p.ProductId).ToArray();
            var products = _context.Products
                .Include(p => p.Images)
                .Where(p => Ids.Contains(p.Id));


            var orderItems = cart.Items.Select(cartItem =>
            {
                var product = products.First(c => c.Id == cartItem.ProductId);

                var orderitem = new OrderItem(
                    product.Id,
                    product.Name,
                    product?.Images?.FirstOrDefault()?.Src ?? "",
                    product.Price,
                    cartItem.Quantity);
                return orderitem;

            }).ToList();

            var userAddress = _context.UserAddresses.SingleOrDefault(p => p.Id == UserAddressId);
            var address = mapper.Map<Address>(userAddress);
            var order = new Order(cart.BuyerId, address, orderItems, paymentMethod);
            _context.Orders.Add(order);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return order.Id;

        }
    }
}
