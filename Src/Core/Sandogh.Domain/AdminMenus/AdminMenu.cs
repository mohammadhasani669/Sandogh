using Sandogh.Domain.Common;
using System.Collections.Generic;

namespace Sandogh.Domain.AdminMenu
{
    public class AdminMenu : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
     
        public List<AdminMenu> SubMenu { get; set; }
    }
}

