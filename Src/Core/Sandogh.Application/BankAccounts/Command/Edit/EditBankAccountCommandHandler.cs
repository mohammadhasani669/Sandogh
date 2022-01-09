using MediatR;
using Sandogh.Application.BankAccounts.Repository;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Edit
{
    internal class EditBankAccountCommandHandler : RequestHandler<EditBankAccountCommand, bool>
    {
        private readonly IBankAccount _bankAccount;

        public EditBankAccountCommandHandler(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        protected override bool Handle(EditBankAccountCommand request)
        {
            var bankAccount = new BankAccount
            {
                AccountNumber = request.AccountNumber,
                BankBranch = request.BankBranch,
                BankName = request.BankName,
                CardNumber = request.CardNumber,
                PersonID = request.PersonID,
                Id = request.Id,
            };
            _bankAccount.Update(bankAccount);
            _bankAccount.SaveChanges();
            return true;
        }
    }
}
