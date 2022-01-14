using Sandogh.Domain.Transactions;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Transactions
{
    public class TransactionService : EfRepository<Transaction>, ITransaction
    {
        public TransactionService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
