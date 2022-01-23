using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Users.Command.Add
{
    public class AddUserAddressValidator : AbstractValidator<AddUserAddressCommand>
    {
        public AddUserAddressValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("الزامیست");
            RuleFor(x => x.State).NotEmpty().WithMessage("الزامیست");
            RuleFor(x => x.PostalAddress).NotEmpty().WithMessage("الزامیست");
            RuleFor(x => x.ReciverName).NotEmpty().WithMessage("الزامیست");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("الزامیست");
        }
    }
}
