using AutoMapper;
using MediatR;
using Sandogh.Domain.AdminMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.AdminMenus.Command.AddToChild
{
    public class AddToChildCommandHandler : RequestHandler<AddChildToMenuCommand, int>
    {
        private readonly IAdminMenu _adminMenu;
        private readonly IMapper _mapper;

        public AddToChildCommandHandler(IAdminMenu adminMenu, IMapper mapper)
        {
            _adminMenu = adminMenu;
            _mapper = mapper;
        }

        protected override int Handle(AddChildToMenuCommand request)
        {

            //its better include childs and add new child in service and user it here

            var parent = _adminMenu.Get(request.ParentId ?? default(int));
            var child = _mapper.Map<AdminMenu>(request);
            parent.SubMenu = new List<AdminMenu>() { child };
            _adminMenu.Update(parent);
            return 0;
        }
    }
}
