using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Products.Command.Add
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("نام کاتالوگ اجباری است");
            RuleFor(x => x.Name).Length(2, 100).WithMessage("نام محصول باید بین 2 تا 100 کاراکتر باشد");
            RuleFor(x => x.Description).NotNull().WithMessage("توضیحات نمی تواند خالی باشد");
            RuleFor(x => x.AvailableStock).NotNull().WithMessage(" نمی تواند خالی باشد").InclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Price).InclusiveBetween(0, int.MaxValue).WithMessage("نمی تواند منفی باشد");
            RuleFor(x => x.Price).NotNull().WithMessage("نمی تواند خالی باشد");
            RuleFor(x => x.MaxStockThreshold).NotNull().WithMessage("نمی تواند خالی باشد");
            RuleFor(x => x.RestockThreshold).NotNull().WithMessage("نمی تواند خالی باشد");
        }
    }
}
