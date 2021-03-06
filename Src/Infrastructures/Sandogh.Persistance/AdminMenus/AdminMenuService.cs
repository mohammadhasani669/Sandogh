using Microsoft.EntityFrameworkCore;
using Sandogh.Domain.AdminMenu;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System.Linq;

namespace Sandogh.Persistance.AdminMenus
{
    public class AdminMenuService : EfRepository<AdminMenu>, IAdminMenu
    {
        private readonly DatabaseContext _databaseContext;
        int level = 0;
        public AdminMenuService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
            _databaseContext = dataBaseContext;
        }

        public int GetLevel(int parentId)
        {
            var p = _databaseContext.AdminMenus.AsNoTracking().FirstOrDefault(x => x.Id == parentId)?.ParentId;
            if (p == null)
                return ++level;
            else
            {

                ++level;
                return GetLevel(p ?? default(int));
            }
        }

        public int GetLevelByParent(int ParentId)
        {
            var p = _databaseContext.AdminMenus.AsNoTracking().SingleOrDefault(x => x.Id == ParentId)?.ParentId;
            if (p == null)
                return level;
            else
            {
                ++level;
                return GetLevelByParent(p ?? default(int));
            }
        }
    }
}
