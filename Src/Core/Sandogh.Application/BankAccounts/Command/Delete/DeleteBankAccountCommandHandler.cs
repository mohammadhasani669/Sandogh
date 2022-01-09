using MediatR;
using Sandogh.Application.BankAccounts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Delete
{
    public class DeleteBankAccountCommandHandler : RequestHandler<DeleteBankAccountCommand, bool>
    {
        private readonly IBankAccount _bankAccount;

        public DeleteBankAccountCommandHandler(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        protected override bool Handle(DeleteBankAccountCommand request)
        {
            var bankAccount = _bankAccount.Get(request.Id);
            if (bankAccount != null)
            {
                _bankAccount.Remove(bankAccount);
                _bankAccount.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
