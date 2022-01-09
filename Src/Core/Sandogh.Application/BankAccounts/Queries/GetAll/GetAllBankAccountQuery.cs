using MediatR;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Queries.GetAll
{
    public class GetAllBankAccountQuery :IRequest<IEnumerable<BankAccount>>
    {
    }
}
