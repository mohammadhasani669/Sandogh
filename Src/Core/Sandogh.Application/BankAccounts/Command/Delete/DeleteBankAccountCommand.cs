using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Delete
{
    public class DeleteBankAccountCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}
