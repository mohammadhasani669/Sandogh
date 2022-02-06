using Sandogh.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sandogh.Domain.AdminMenu
{
    public class AdminMenu : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual AdminMenu Parent { get; set; }

        public List<AdminMenu> SubMenu { get; set; } 


        public void AddSub(AdminMenu menu)
        {
            SubMenu.Add(menu);
        }
    }
}

