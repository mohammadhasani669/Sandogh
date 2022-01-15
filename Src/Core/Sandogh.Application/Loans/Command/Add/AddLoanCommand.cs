using MediatR;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Loans.Command.Add
{
    public class AddLoanCommand : IRequest<int>
    {
    }
}
