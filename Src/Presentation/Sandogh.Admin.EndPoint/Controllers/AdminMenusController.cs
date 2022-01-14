using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Application.AdminMenus.Command.Add;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class AdminMenusController : Controller
    {
        private readonly IMediator _mediator;

        public AdminMenusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddAdminMenuCommand addAdminMenuCommand)
        {
            var result = _mediator.Send(addAdminMenuCommand);
            return View(nameof(Index));
        }
    }
}
