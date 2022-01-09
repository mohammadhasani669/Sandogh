using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Domain.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandogh.Application.BankAccounts.Command.Add
{
    public class AddBankAccountCommand : IRequest<int>
    {
        public int PersonID { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public List<SelectListItem> People { get; set; }
    }
}
