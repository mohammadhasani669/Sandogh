using MediatR;
using Sandogh.Common;
using Sandogh.Domain.BankAccounts;

namespace Sandogh.Application.BankAccounts.Queries.GetAll
{
    public class GetAllBankAccountQuery :IRequest<PaginatedItemsDto<BankAccount>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string Search { get; set; }
    }
}
