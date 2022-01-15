using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Application.Products.Command.Add;
using Sandogh.Domain.Products;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductBrand _productBrand;
        private readonly IProductCategory _productCategory;
        private readonly IProductSize _productSize;

        public ProductsController(IMediator mediator, IProductBrand productBrand, IProductCategory productCategory, IProductSize productSize)
        {
            _mediator = mediator;
            _productBrand = productBrand;
            _productCategory = productCategory;
            _productSize = productSize;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
           
            ViewBag.Brand_ddl = new SelectList(_productBrand.GetAll(), "Name", "Id");
            ViewBag.ProductCategory_ddl = new SelectList(_productCategory.GetAll(), "Name", "Id");
            ViewBag.ProductSize = new SelectList(_productSize.GetAll(), "Name", "Id");
          
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddProductCommand addProductCommand)
        {
            var result = _mediator.Send(addProductCommand);
            return View();
        }
    }
}
