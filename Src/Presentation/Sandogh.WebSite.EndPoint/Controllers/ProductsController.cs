using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Application.Products.Queries.GetAll;
using Sandogh.Application.Products.Queries.PDP;

namespace Sandogh.WebSite.EndPoint.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(string search = "", int pageNumber = 1, int pageSize = 2)
        {
            GetAllProductQuery getAll = new GetAllProductQuery();
            getAll.pageSize = pageSize;
            getAll.page = pageNumber;
            getAll.search = search;
            var result = _mediator.Send(getAll).Result;

            return View(result);
        }

        public IActionResult Details(int Id)
        {
            PDPQuery pDPQuery =  new PDPQuery { Id = Id };
            var data = _mediator.Send(pDPQuery).Result;
            return View(data);
        }
    }
}
