using Sandogh.Domain.Common;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.LoanRepayments
{
    public class LoanRepayment : Entity
    {
        public int LoanId { get; set; }
        public int Amount { get; set; }
        public DateTime ReciveDate { get; set; }

        public  Loan Loan { get; set; }
    }
}
