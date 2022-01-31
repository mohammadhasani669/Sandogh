using MediatR;
using Sandogh.Domain.AdminMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.AdminMenus.Queries.GetAll
{
    public class GetAllAdminMenuQuery : IRequest<IEnumerable<GetAllAdminMenuQuery>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<AdminMenu> SubMenu { get; set; }
    }
}
