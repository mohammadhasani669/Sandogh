using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class HomeHeaderUpperVC:ViewComponent
    {
        public HomeHeaderUpperVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            
            return View();
        }
    }
}
