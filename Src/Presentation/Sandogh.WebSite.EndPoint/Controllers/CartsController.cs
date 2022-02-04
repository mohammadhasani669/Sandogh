using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Orders;
using Sandogh.Domain.Users;
using Sandogh.WebSite.EndPoint.Models.ViewModels.Carts;
using System;
using System.Security.Claims;

namespace Sandogh.WebSite.EndPoint.Controllers
{
    public class CartsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICart _cart;
        private readonly IUserAddress _userAddress;
        private readonly IOrder _orderService;
        string UserId = null;

        public CartsController(SignInManager<ApplicationUser> signInManager,
                     ICart cart, IUserAddress userAddress, IOrder order)
        {
            _signInManager = signInManager;
            _cart = cart;
            _userAddress = userAddress;
            _orderService = order;
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
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveItemFromCart(int ItemId)
        {
            var result = _cart.RemoveItemFromCart(ItemId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SetQuantities(int ItemId, int qty)
        {
            return Json(_cart.SetQuantities(ItemId, qty));
            
        }

        private CartDto GetOrSetCart()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _cart.GetOrCreateBasketForUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
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


        [Authorize]
        public IActionResult ShippingPayment()
        {
            ShippingPaymentViewModel model = new ShippingPaymentViewModel();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.Basket = _cart.GetBasketForUser(userId);
            model.UserAddresses = _userAddress.GetAddress(userId);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ShippingPayment(int Address, PaymentMethod PaymentMethod)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _cart.GetBasketForUser(userId);
            int orderId = _orderService.CreateOrder(cart.Id, Address, PaymentMethod);
            if (PaymentMethod == PaymentMethod.OnlinePaymnt)
            {
                //ثبت پرداخت
                // var payment = _orderService.PayForOrder(orderId);
                //ارسال به درگاه پرداخت
                return RedirectToAction("Index", "Pay", new
                {
                    PaymentId = 0// payment.PaymentId
                });
            }
            else
            {
                //برو به صفحه سفارشات من
                return RedirectToAction("Index", "Orders", new { area = "customers" });
            }
        }
    }
}
