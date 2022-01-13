using Sandogh.Application.Common;
using Sandogh.Application.Interfaces;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Repository
{
    public interface IBankAccount : IRepository<BankAccount>
    {
        PagedData<BankAccount> GetByPaging(int pageNumber, int pageSize, string search);
    }

    public class BankAccountService : EfRepository<BankAccount>, IBankAccount
    {
        private readonly IDataBaseContext _context;
        public BankAccountService(IDataBaseContext dataBaseContext) : base(dataBaseContext)
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
