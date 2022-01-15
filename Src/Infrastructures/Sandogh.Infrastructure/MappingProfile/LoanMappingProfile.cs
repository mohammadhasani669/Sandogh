using AutoMapper;
using Sandogh.Application.Loans.Command.Add;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.MappingProfile
{
    public class LoanMappingProfile : Profile
    {
        public LoanMappingProfile()
        {
            CreateMap<Loan, AddLoanCommand>().ReverseMap();
        }
    }
}
