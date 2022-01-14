using Sandogh.Common;
using Sandogh.Domain.Common;

namespace Sandogh.Domain.BankAccounts
{
    public interface IBankAccount : IRepository<BankAccount>
    {
        PagedData<BankAccount> GetByPaging(int pageNumber, int pageSize, string search);
    }
}
