using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.AdminMenu
{
    public class AdminMenu:Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<AdminMenu> SubMenu { get; set; }
    }
}

