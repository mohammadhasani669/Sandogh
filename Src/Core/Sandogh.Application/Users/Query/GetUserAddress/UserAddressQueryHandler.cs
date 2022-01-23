using AutoMapper;
using MediatR;
using Sandogh.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Users.Query.GetUserAddress
{
    public class UserAddressQueryHandler : RequestHandler<GetUserAddressQuery, List<UserAddressQuery>>
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public UserAddressQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override List<UserAddressQuery> Handle(GetUserAddressQuery request)
        {
            var address = _context.UserAddresses.Where(x => x.UserId == request.UserId).ToList();
            var data = _mapper.Map<List<UserAddressQuery>>(address);
            return data;
        }
    }
}
