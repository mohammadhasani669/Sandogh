using System.Collections.Generic;

namespace Sandogh.Domain.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

    }
}
