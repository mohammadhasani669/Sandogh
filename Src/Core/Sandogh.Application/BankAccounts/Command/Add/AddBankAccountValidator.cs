using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.BankAccounts.Command.Add
{
    public class AddBankAccountValidator: AbstractValidator<AddBankAccountCommand>
    {
        public AddBankAccountValidator()
        {
            RuleFor(x => x.BankName).NotNull().WithMessage("لطفا نام بانک را وارد کنید");
            RuleFor(x => x.BankBranch).NotNull().WithMessage("لطفا نام شعبه را وارد کنید");
        }
    }
}
