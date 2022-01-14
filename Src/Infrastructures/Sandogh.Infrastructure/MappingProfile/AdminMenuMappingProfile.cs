using AutoMapper;
using Sandogh.Application.AdminMenus.Command.Add;
using Sandogh.Domain.AdminMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.MappingProfile
{
    public class AdminMenuMappingProfile : Profile
    {
        public AdminMenuMappingProfile()
        {
            CreateMap<AdminMenu, AddAdminMenuCommand>().ReverseMap();
        }
    }
}
