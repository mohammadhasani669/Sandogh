using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Users;
using System;

namespace Sandogh.WebSite.EndPoint.Controllers
{
    public class CartsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICart _cart;
        string UserId = null;

        public CartsController(SignInManager<ApplicationUser> signInManager, ICart cart)
        {
            _signInManager = signInManager;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var cart = GetOrSetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Index(int productId, int qty = 1)
        {
            var cart = GetOrSetCart();
            _cart.AddItemToCart(cart.Id, productId, qty);
            return View(nameof(Index));
        }

        private CartDto GetOrSetCart()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _cart.GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                SetCookieForCart();
                return _cart.GetOrCreateBasketForUser(UserId);

            }

        }

        private void SetCookieForCart()
        {
            string CartCookieName = "CartId";
            if (Request.Cookies.ContainsKey(CartCookieName))
            {
                UserId = Request.Cookies[CartCookieName];
            }
            if (UserId != null) return;
            UserId = Guid.NewGuid().ToString();
            var CookieOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Now.AddYears(2)
            };
            Response.Cookies.Append(CartCookieName, UserId, CookieOptions);



        }
    }
}
