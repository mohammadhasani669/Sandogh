using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Edit
{
    public class EditBankAccountCommand :IRequest<bool>
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
    }
}
