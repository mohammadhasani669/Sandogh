using AutoMapper;
using MediatR;
using Sandogh.Domain.AdminMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.AdminMenus.Queries.GetAll
{
    public class GetAllAdminMenuQueryHandler : RequestHandler<GetAllAdminMenuQuery, IEnumerable<GetAllAdminMenuQuery>>
    {
        private readonly IAdminMenu _adminMenu;
        private readonly IMapper _mapper;

        public GetAllAdminMenuQueryHandler(IAdminMenu adminMenu, IMapper mapper)
        {
            _adminMenu = adminMenu;
            _mapper = mapper;
        }

        protected override IEnumerable<GetAllAdminMenuQuery> Handle(GetAllAdminMenuQuery request)
        {
            var MenuList = _adminMenu.GetAll();
            var result = _mapper.Map<IEnumerable<GetAllAdminMenuQuery>>(MenuList);
            return result;
        }
    }
}
