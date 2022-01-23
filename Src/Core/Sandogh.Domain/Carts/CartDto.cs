using System.Collections.Generic;
using System.Linq;

namespace Sandogh.Domain.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public int Total()
        {
            if (Items.Count > 0)
            {
                return Items.Sum(p => p.UnitPrice * p.Quantity);
            }
            return 0;
        }

    }
}
