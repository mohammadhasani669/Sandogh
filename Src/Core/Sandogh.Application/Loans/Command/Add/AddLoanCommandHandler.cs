using AutoMapper;
using MediatR;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Loans.Command.Add
{
    public class AddLoanCommandHandler : RequestHandler<AddLoanCommand, int>
    {
        private readonly ILoan _loan;
        private readonly IMapper _mapper;

        public AddLoanCommandHandler(ILoan loan, IMapper mapper)
        {
            _loan = loan;
            _mapper = mapper;
        }

        protected override int Handle(AddLoanCommand request)
        {
            var loan = _mapper.Map<Loan>(request);
            _loan.Insert(loan);
            return loan.Id;
        }
    }
}
