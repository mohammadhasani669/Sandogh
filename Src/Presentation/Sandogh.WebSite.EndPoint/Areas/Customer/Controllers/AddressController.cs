using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Application.Users.Command.Add;
using Sandogh.Application.Users.Query.GetUserAddress;
using System.Security.Claims;

namespace Sandogh.WebSite.EndPoint.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class AddressController : Controller
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            var req = new GetUserAddressQuery { UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value };
            var result = _mediator.Send(req).Result;
            return View(result);
        }

        public IActionResult AddNewAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAddress(AddUserAddressCommand addUserAddress)
        {
            addUserAddress.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _mediator.Send(addUserAddress).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}
