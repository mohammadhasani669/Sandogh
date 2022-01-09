using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class HeaderTopVC:ViewComponent
    {
        public HeaderTopVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
