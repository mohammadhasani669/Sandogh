using MediatR;
using Sandogh.Application.BankAccounts.Repository;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Add
{
    public class AddBankAccountCommandHandler : RequestHandler<AddBankAccountCommand,int>
    {
        private readonly IBankAccount _bankAccount;

        public AddBankAccountCommandHandler(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        protected override int Handle(AddBankAccountCommand request)
        {
            var bankAccount =  new BankAccount 
            {
                AccountNumber = request.AccountNumber,
                BankBranch = request.BankBranch,
                BankName = request.BankName,
                CardNumber = request.CardNumber,
                PersonID = request.PersonID,
                
            };
            _bankAccount.Insert(bankAccount);
            _bankAccount.SaveChanges();
            return bankAccount.Id;
        }
    }
}
