using Sandogh.Domain.Common;
using Sandogh.Domain.LoanRepayments;
using Sandogh.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Loans
{
    public class Loan : Entity
    {
        public int PersonId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public  Person Person { get; set; }
        public List<LoanRepayment> LoanRepayments { get; set; }
    }
}
