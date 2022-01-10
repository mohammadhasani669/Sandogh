using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class FooterVC:ViewComponent
    {
        public FooterVC()
        {
           
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
