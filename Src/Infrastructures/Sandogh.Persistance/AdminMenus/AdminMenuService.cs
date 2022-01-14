using Sandogh.Domain.AdminMenu;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;

namespace Sandogh.Persistance.AdminMenus
{
    public class AdminMenuService : EfRepository<AdminMenu>, IAdminMenu
    {
        public AdminMenuService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
