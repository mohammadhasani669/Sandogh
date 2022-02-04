using AutoMapper;
using Sandogh.Common;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections;
using System.Linq;

namespace Sandogh.Domain.BankAccounts
{
    public class BankAccountService : EfRepository<BankAccount>, IBankAccount
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public BankAccountService(DatabaseContext dataBaseContext, IMapper mapper) : base(dataBaseContext)
        {
            _context = dataBaseContext;
            _mapper = mapper;
        }

        public PaginatedItemsDto<BankAccount> GetByPaging(int pageNumber, int pageSize, string search)
        {
            int totalCount = 0;
            var model = _context.BankAccounts
                .Where(x => x.BankBranch.Contains(search) || string.IsNullOrWhiteSpace(search))
                .PagedResult(pageNumber,pageSize,out totalCount).ToList();

            //var result = _mapper.ProjectTo<CatalogTypeListDto>(model).ToList();
                     
            return new PaginatedItemsDto<BankAccount>(pageNumber,pageSize, totalCount, model);
        }
    }
}
