using MediatR;
using Sandogh.Domain.AdminMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.AdminMenus.Command.Add
{
    public class AddAdminMenuCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<AdminMenu> SubMenu { get; set; }
    }
}
