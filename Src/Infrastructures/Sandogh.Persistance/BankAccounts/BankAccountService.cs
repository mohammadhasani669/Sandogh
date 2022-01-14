using Sandogh.Common;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Linq;

namespace Sandogh.Domain.BankAccounts
{
    public class BankAccountService : EfRepository<BankAccount>, IBankAccount
    {
        private readonly DatabaseContext _context;
        public BankAccountService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public PagedData<BankAccount> GetByPaging(int pageNumber, int pageSize, string search)
        {
            var result = new PagedData<BankAccount>()
            {
                PageInfo = new PageInfo()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            };

            result.Data = _context.bankAccounts
                .Where(x => x.BankBranch.Contains(search) || string.IsNullOrWhiteSpace(search))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            if (result.Data.Count() == 0)
            {
                result.PageInfo.PageNumber = 1;

                result.Data = _context.bankAccounts
               .Where(x => x.BankBranch.Contains(search) || string.IsNullOrWhiteSpace(search))
               .Skip((1 - 1) * pageSize)
               .Take(pageSize).ToList();
            }

            result.PageInfo.TotalCount = _context.bankAccounts
                .Where(x => x.BankBranch.Contains(search) || string.IsNullOrWhiteSpace(search)).Count();
            return result;
        }
    }
}
