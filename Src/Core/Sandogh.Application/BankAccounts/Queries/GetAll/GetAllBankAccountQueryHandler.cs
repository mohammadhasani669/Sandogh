﻿using MediatR;
using Sandogh.Common;
using Sandogh.Domain.BankAccounts;

namespace Sandogh.Application.BankAccounts.Queries.GetAll
{
    public class GetAllBankAccountQueryHandler : RequestHandler<GetAllBankAccountQuery, PagedData<BankAccount>>
    {
        private readonly IBankAccount _bankAccount;

        public GetAllBankAccountQueryHandler(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        protected override PagedData<BankAccount> Handle(GetAllBankAccountQuery request)
        {
            var list = _bankAccount.GetByPaging(request.pageNumber, request.pageSize,request.Search);
            return list;
        }
    }
}
