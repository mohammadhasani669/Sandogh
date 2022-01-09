using Microsoft.AspNetCore.Mvc;

namespace Sandogh.Admin.EndPoint.Components
{
    public class SidebarVC:ViewComponent
    {
        public SidebarVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
