using AutoMapper;
using MediatR;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Loans.Command.Edit
{
    public class EditLoanCommandHandler : RequestHandler<EditLoanCommand, int>
    {
        private readonly ILoan _loan;
        private readonly IMapper _mapper;

        public EditLoanCommandHandler(ILoan loan, IMapper mapper)
        {
            _loan = loan;
            _mapper = mapper;
        }

        protected override int Handle(EditLoanCommand request)
        {
            var loan = _mapper.Map<Loan>(request);
            _loan.Update(loan);
            return loan.Id;
        }
    }
}
