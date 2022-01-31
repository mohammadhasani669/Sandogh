using Microsoft.AspNetCore.Mvc;

namespace Sandogh.WebSite.EndPoint.Controllers
{
    public class AdminMenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

      
    }
}
