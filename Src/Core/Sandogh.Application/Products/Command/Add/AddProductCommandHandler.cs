using AutoMapper;
using MediatR;
using Sandogh.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Products.Command.Add
{
    public class AddProductCommandHandler : RequestHandler<AddProductCommand, int>
    {
        private readonly IProduct _product;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IProduct product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        protected override int Handle(AddProductCommand request)
        {
            var product = _mapper.Map<Product>(request);
            _product.Insert(product);
            return product.Id;
        }
    }
}
