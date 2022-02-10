using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sandogh.Admin.EndPoint.Models.VIewModels.Roles;

namespace Sandogh.Admin.EndPoint.Security.IdentityService
{
    public interface IUtilities
    {
        public IList<ActionAndControllerName> AreaAndActionAndControllerNamesList();
        public IList<string> GetAllAreasNames();

    }
}
