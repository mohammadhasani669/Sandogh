using Microsoft.AspNetCore.Mvc;

namespace Sandogh.Admin.EndPoint.Components
{
    public class NavBarVC : ViewComponent
    {
        public NavBarVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
