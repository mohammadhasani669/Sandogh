using Sandogh.Domain.BankProfits;
using Sandogh.Domain.Common;
using Sandogh.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.BankAccounts
{
    public class BankAccount : Entity
    {
        public int PersonID { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }

        public Person Person { get; set; }
        public List<BankProfit> BankProfits { get; set; }
    }
}
