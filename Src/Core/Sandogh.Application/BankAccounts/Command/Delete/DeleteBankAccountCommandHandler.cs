using MediatR;
using Sandogh.Domain.BankAccounts;
using System.Collections.Generic;

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
