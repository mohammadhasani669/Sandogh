using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Orders
{
    public interface IOrder : IRepository<Order>
    {
        int CreateOrder(int cartId, int UserAddressId, PaymentMethod paymentMethod);

    }
}
