using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class HeaderUpperVC : ViewComponent
    {
        public HeaderUpperVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
