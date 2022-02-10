using AutoMapper;
using DNTBreadCrumb.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Application.AdminMenus.Command.Add;
using Sandogh.Application.AdminMenus.Command.AddToChild;
using Sandogh.Application.AdminMenus.Queries.GetAll;
using Sandogh.Domain.AdminMenu;
using System.Collections.Generic;

namespace Sandogh.Admin.EndPoint.Controllers
{
    [BreadCrumb(Title = "منوها", UseDefaultRouteUrl = true,Order = 0, IgnoreAjaxRequests = true,Url ="/")]
    public class AdminMenusController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAdminMenu _adminMenu;
        private readonly IMapper _mapper;

        public AdminMenusController(IMediator mediator, IAdminMenu adminMenu, IMapper mapper)
        {
            _mediator = mediator;
            _adminMenu = adminMenu;
            _mapper = mapper;
        }

        [BreadCrumb(Title = "لیست", Order = 1, IgnoreAjaxRequests = true,Url ="AdminMenus/index")]
        public IActionResult Index()
        {
            var req = new GetAllAdminMenuQuery();
            var result = _mediator.Send(req).Result;
            return View(result);
        }


        public IActionResult AddAchildToMenu(int parentId)
        {
            var parent = _adminMenu.Get(parentId);
            var rseult = _mapper.Map<AddChildToMenuCommand>(parent);
            return View(rseult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAchildToMenu(AddChildToMenuCommand model)
        {
            var result = _mediator.Send(model).Result;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddAdminMenuCommand addAdminMenuCommand)
        {
            var result = _mediator.Send(addAdminMenuCommand);
            return RedirectToAction(nameof(Index));
        }
    }
}
