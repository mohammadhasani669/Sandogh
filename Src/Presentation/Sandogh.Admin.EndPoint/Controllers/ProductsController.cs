using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Application.Products.Command.Add;
using Sandogh.Domain.Products;
using System.Collections.Generic;

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
            ViewBag.Brands_ddl = new SelectList(_productBrand.GetAllForDDL(), "Id", "Name");
            ViewBag.ProductCategory_ddl = new SelectList(_productCategory.GetAllForDDL(), "Id", "Name");
            ViewBag.ProductSizees_ddl = new SelectList(_productSize.GetAllForDDL(), "Id", "Name");
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProductCommand addProductCommand,IList<IFormFile> files)
        {
            var result = _mediator.Send(addProductCommand);
            return new JsonResult(new { isSuccess = true } );
        }
    }
}
