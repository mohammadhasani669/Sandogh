using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class HomeHeaderLowerVC:ViewComponent
    {
        public HomeHeaderLowerVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
