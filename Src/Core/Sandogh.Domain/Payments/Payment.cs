using Sandogh.Domain.Common;
using Sandogh.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Payments
{
    public class Payment : Entity
    {
        public int Amount { get; private set; }
        public bool IsPay { get; private set; } = false;
        public DateTime? DatePay { get; private set; } = null;
        public string Authority { get; private set; }
        public long RefId { get; private set; } = 0;
        public Order Order { get; private set; }
        public int OrderId { get; private set; }

        public Payment(int amount, int orderId)
        {
            Amount = amount;
            OrderId = orderId;
        }

        public void PaymentIsDone(string authority, long refId)
        {
            IsPay = true;
            DatePay = DateTime.Now;
            Authority = authority;
            RefId = refId;
        }
    }
}
