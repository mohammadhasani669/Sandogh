using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.AdminMenus.Command.AddToChild
{
    public class AddChildToMenuCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
