using AutoMapper;
using Sandogh.Application.AdminMenus.Command.Add;
using Sandogh.Application.AdminMenus.Command.AddToChild;
using Sandogh.Application.AdminMenus.Queries.GetAll;
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
            CreateMap<AdminMenu, GetAllAdminMenuQuery>().ReverseMap();
            CreateMap<AdminMenu, AddChildToMenuCommand>().ReverseMap();
        }
    }
}
