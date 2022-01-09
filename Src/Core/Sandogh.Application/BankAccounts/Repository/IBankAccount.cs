using Sandogh.Application.Interfaces;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Repository
{
    public interface IBankAccount : IRepository<BankAccount>
    {
    }

    public class BankAccountService : EfRepository<BankAccount>, IBankAccount
    {
        public BankAccountService(IDataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
