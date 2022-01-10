using AutoMapper;
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
        private readonly IMapper _mapper;

        public AddBankAccountCommandHandler(IBankAccount bankAccount, IMapper mapper)
        {
            _bankAccount = bankAccount;
            _mapper = mapper;
        }

        protected override int Handle(AddBankAccountCommand request)
        {
            var bankAccount = _mapper.Map<BankAccount>(request);
            _bankAccount.Insert(bankAccount);
            _bankAccount.SaveChanges();
            return bankAccount.Id;
        }
    }
}
