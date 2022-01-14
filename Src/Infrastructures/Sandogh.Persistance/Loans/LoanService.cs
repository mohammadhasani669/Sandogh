using Sandogh.Domain.Loans;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Loans
{
    public class LoanService : EfRepository<Loan>, ILoan
    {
        public LoanService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
