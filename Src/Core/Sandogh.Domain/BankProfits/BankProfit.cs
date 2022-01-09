using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.BankProfits
{
    public class BankProfit : Entity
    {
        public int BankAccountID { get; set; }

        public DateTime Date { get; set; }

        public long Turnover { get; set; }

        public long Balance { get; set; }

        public  BankAccount BankAccount { get; set; }
    }
}
