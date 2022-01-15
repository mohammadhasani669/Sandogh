using AutoMapper;
using MediatR;
using Sandogh.Domain.AdminMenu;

namespace Sandogh.Application.AdminMenus.Command.Add
{
    public class AddAdminMenuCommandHandler : RequestHandler<AddAdminMenuCommand, int>
    {
        private readonly IAdminMenu _adminMenu;
        private readonly IMapper _mapper;

        public AddAdminMenuCommandHandler(IAdminMenu adminMenu, IMapper mapper)
        {
            _adminMenu = adminMenu;
            _mapper = mapper;
        }

        protected override int Handle(AddAdminMenuCommand request)
        {
            var AdminMenu = _mapper.Map<AdminMenu>(request);
            _adminMenu.Insert(AdminMenu);
            return AdminMenu.Id;
        }
    }
}
