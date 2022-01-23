using AutoMapper;
using MediatR;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Users.Command.Add
{
    public class AddUserAddressCommandHandler : RequestHandler<AddUserAddressCommand, int>
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddUserAddressCommandHandler(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        protected override int Handle(AddUserAddressCommand request)
        {
           var data= _mapper.Map<UserAddress>(request);
            _context.UserAddresses.Add(data);
            _context.SaveChanges();
            return data.Id;
        }
    }
}
