using AutoMapper;
using Sandogh.Domain.Users;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Users
{
    public class UserAddressService : EfRepository<UserAddress>, IUserAddress
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserAddressService(DatabaseContext dataBaseContext, IMapper mapper) : base(dataBaseContext)
        {
            _context = dataBaseContext;
            _mapper = mapper;
        }

        public void AddnewAddress(AddUserAddressDto address)
        {
            var data = _mapper.Map<UserAddress>(address);
            _context.UserAddresses.Add(data);
            _context.SaveChanges();
        }

        public List<UserAddressDto> GetAddress(string UserId)
        {
            var address = _context.UserAddresses.Where(p => p.UserId == UserId);
            var data = _mapper.Map<List<UserAddressDto>>(address);
            return data;
        }
    }
}
