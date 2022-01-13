using MediatR;
using Sandogh.Application.Common;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Queries.GetAll
{
    public class GetAllBankAccountQuery :IRequest<PagedData<BankAccount>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string Search { get; set; }
    }
}
