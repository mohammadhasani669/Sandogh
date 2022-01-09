using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Components
{
    public class StickyheaderVC:ViewComponent
    {
        public StickyheaderVC()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
