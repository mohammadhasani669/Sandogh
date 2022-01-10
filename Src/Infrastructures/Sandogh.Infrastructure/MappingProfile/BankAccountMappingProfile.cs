using AutoMapper;
using Sandogh.Application.BankAccounts.Command.Add;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.MappingProfile
{
    public class BankAccountMappingProfile:Profile
    {
        public BankAccountMappingProfile()
        {
            CreateMap<BankAccount,AddBankAccountCommand>().ReverseMap();
        }
    }
}
