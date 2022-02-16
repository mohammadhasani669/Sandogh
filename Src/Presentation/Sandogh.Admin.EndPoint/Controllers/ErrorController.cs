using Microsoft.AspNetCore.Mvc;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
