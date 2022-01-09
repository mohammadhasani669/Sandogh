using MediatR;
using Sandogh.Application.BankAccounts.Repository;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Queries.GetAll
{
    public class GetAllBankAccountQueryHandler : RequestHandler<GetAllBankAccountQuery, IEnumerable<BankAccount>>
    {
        private readonly IBankAccount _bankAccount;

        public GetAllBankAccountQueryHandler(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        protected override IEnumerable<BankAccount> Handle(GetAllBankAccountQuery request)
        {
            var list = _bankAccount.GetAll();
            return list;
        }
    }
}
