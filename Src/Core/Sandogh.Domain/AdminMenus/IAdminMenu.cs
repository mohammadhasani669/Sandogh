using Sandogh.Domain.Common;

namespace Sandogh.Domain.AdminMenu
{
    public interface IAdminMenu : IRepository<AdminMenu>
    {
        int GetLevel(int parentId);
    }
}
