using AutoMapper;
using Sandogh.Application.Users.Command.Add;
using Sandogh.Application.Users.Query.GetUserAddress;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserAddress, UserAddressQuery>().ReverseMap();
            CreateMap<AddUserAddressCommand, UserAddress>().ReverseMap();
        }
    }
}
