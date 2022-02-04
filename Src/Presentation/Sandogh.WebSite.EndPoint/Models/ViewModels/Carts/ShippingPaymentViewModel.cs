using Sandogh.Domain.Carts;
using Sandogh.Domain.Users;
using System.Collections.Generic;

namespace Sandogh.WebSite.EndPoint.Models.ViewModels.Carts
{
    public class ShippingPaymentViewModel
    {
        public CartDto Basket { get; set; }
        public List<UserAddressDto> UserAddresses { get; set; }
    }
}
