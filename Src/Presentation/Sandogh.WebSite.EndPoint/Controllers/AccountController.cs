using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Application.Emails;
using Sandogh.Common;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Emails;
using Sandogh.Domain.Users;
using Sandogh.WebSite.EndPoint.Models.ViewModels.Account;
using System.Security.Claims;

namespace Sandogh.WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly Configs _configs;
        private readonly ICart _cart;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, Configs configs, ICart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configs = configs;
            _cart = cart;
        }

        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            //g-Recaptcha-Response
            string googleResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            GoogleRecaptcha googleRecaptcha = new GoogleRecaptcha(_configs);
            if (googleRecaptcha.Verify(googleResponse) == false)
            {
                ModelState.AddModelError("", "لطفا بر روی دکمه من ربات نیستم کلیک نمایید");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (CheckLoginjection(model))
                return View(model);

            model.Email = model.Email.PersianToEnglish();
            model.Password = model.Password.PersianToEnglish();

            var user = _userManager.FindByNameAsync(model.Email).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View();
            }
            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true).Result;
            if (result.Succeeded)
            {
                TransferBasketForuser(user.Id);
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.RequiresTwoFactor)
            {

            }
            if (result.IsLockedOut)
            {

            }
            if (user.EmailConfirmed == false)
            {
                ModelState.AddModelError("", "ایمیل شما تایید نشده است لطفا به ایمیل خود مراجعه کنید و روی لینک فعالسازی کلیک کنید");
                return View(model);
            }
            if (user.PhoneNumberConfirmed)
            {

            }

            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است");

            return View(model);
        }

        private bool CheckLoginjection(LoginViewModel model)
        {
            bool IsInjection = false;
            CheckInjection checkInjection = new CheckInjection();
            if (checkInjection.IsInjection(model.Email))
            {
                //log user info and ip for persue
                IsInjection = true;
                return IsInjection;
            }
            if (checkInjection.IsInjection(model.Password))
            {
                IsInjection = true;
                return IsInjection;
            }
            return false;
        }

        private bool CheckRegisterInjection(RegisterViewModel model)
        {
            bool IsInjection = false;
            CheckInjection checkInjection = new CheckInjection();
            if (checkInjection.IsInjection(model.Email))
            {
                //log user info and ip for persue
                IsInjection = true;
                return IsInjection;
            }
            if (checkInjection.IsInjection(model.Password))
            {
                IsInjection = true;
                return IsInjection;
            }
            if (checkInjection.IsInjection(model.PhoneNumber))
            {
                IsInjection = true;
                return IsInjection;
            }
            if (checkInjection.IsInjection(model.FullName))
            {
                IsInjection = true;
                return IsInjection;
            }

            return false;
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //g-Recaptcha-Response
            string googleResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            GoogleRecaptcha googleRecaptcha = new GoogleRecaptcha(_configs);
            if (googleRecaptcha.Verify(googleResponse) == false)
            {
                ModelState.AddModelError("", "لطفا بر روی دکمه من ربات نیستم کلیک نمایید");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (CheckRegisterInjection(model))
            {
                return View(model);
            }


            ApplicationUser user = new ApplicationUser();

            user.UserName = model.Email.PersianToEnglish();
            user.Email = model.Email.PersianToEnglish();
            user.FullName = model.FullName.PersianToEnglish();
            user.PhoneNumber = model.PhoneNumber.PersianToEnglish();

            model.Password = model.Password.PersianToEnglish();

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                TransferBasketForuser(user.Id); 
                var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                string callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    UserId = user.Id,
                    token = token
                }, protocol: Request.Scheme);

                string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                //_emailService.Execute(user.Email, body, "فعال سازی حساب کاربری باگتو");
                _signInManager.SignInAsync(user, isPersistent: true).Wait();
                return RedirectToAction(nameof(Profile));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View(model);
        }

        public IActionResult ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
            {
                return View("Error");
            }

            var result = _userManager.ConfirmEmailAsync(user, Token).Result;
            if (result.Succeeded)
            {
                /// return 
            }
            else
            {

            }
            return RedirectToAction("login");

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }

            var user = _userManager.FindByEmailAsync(forgot.Email).Result;
            if (user == null || _userManager.IsEmailConfirmedAsync(user).Result == false)
            {
                ViewBag.meesage = "ممکن است ایمیل وارد شده معتبر نباشد! و یا اینکه ایمیل خود را تایید نکرده باشید";
                return View();
            }

            string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            string callbakUrl = Url.Action("ResetPassword", "Account", new
            {
                UserId = user.Id,
                token = token
            }, protocol: Request.Scheme);

            string body = $"برای تنظیم مجدد کلمه عبور بر روی لینک زیر کلیک کنید <br/> <a href={callbakUrl}> link reset Password </a>";

            var email = new Email
            {
                To = user.Email,
                Body = body,
                Subject = "فراموشی رمز عبور"
            };
            
            _emailService.Send(email, _configs.EmailPassword);
            ViewBag.meesage = "لینک تنظیم مجدد کلمه عبور برای ایمیل شما ارسال شد";
            return View();
        }

        public IActionResult ResetPassword(string UserId, string Token)
        {
            return View(new ResetPasswordViewModel
            {
                Token = Token,
                UserId = UserId,
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);
            if (reset.Password != reset.ConfirmPassword)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(reset.UserId).Result;
            if (user == null)
            {
                return BadRequest();
            }

            var Result = _userManager.ResetPasswordAsync(user, reset.Token, reset.Password).Result;

            if (Result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            }
            else
            {
                ViewBag.Errors = Result.Errors;
                return View(reset);
            }

        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        public IActionResult SetPhoneNumber()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetPhoneNumber(SetPhoneNumberViewModel phoneNumberDro)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var setResult = _userManager.SetPhoneNumberAsync(user, phoneNumberDro.PhoneNumber).Result;
            string code = _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumberDro.PhoneNumber).Result;
            //SmsService smsService = new SmsService();
            //smsService.Send(phoneNumberDro.PhoneNumber, code);
            TempData["PhoneNumber"] = phoneNumberDro.PhoneNumber;
            return RedirectToAction(nameof(VerifyPhoneNumber));
        }

        [Authorize]
        public IActionResult VerifyPhoneNumber()
        {

            return View(new VerifyPhoneNumberViewModel
            {
                PhoneNumber = TempData["PhoneNumber"].ToString(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult VerifyPhoneNumber(VerifyPhoneNumberViewModel verify)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool resultVerify = _userManager.VerifyChangePhoneNumberTokenAsync(user, verify.Code, verify.PhoneNumber).Result;
            if (resultVerify == false)
            {
                ViewData["Message"] = $"کد وارد شده برای شماره {verify.PhoneNumber} اشتباه اشت";
                return View(verify);
            }
            else
            {
                user.PhoneNumberConfirmed = true;
                _userManager.UpdateAsync(user);
            }
            return RedirectToAction("VerifySuccess");

        }


        public IActionResult VerifySuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult ExternalLogin(string ReturnUrl)
        {
            string url = Url.Action(nameof(CallBack), "Account", new
            {
                ReturnUrl
            });

            var propertis = _signInManager
                .ConfigureExternalAuthenticationProperties("Google", url);

            return new ChallengeResult("Google", propertis);
        }

        public IActionResult CallBack(string ReturnUrl)
        {
            var loginInfo = _signInManager.GetExternalLoginInfoAsync().Result;

            string email = loginInfo.Principal.FindFirst(ClaimTypes.Email)?.Value ?? null;
            if (email == null)
            {
                return BadRequest();
            }
            string FirstName = loginInfo.Principal.FindFirst(ClaimTypes.GivenName)?.Value ?? null;
            string LastName = loginInfo.Principal.FindFirst(ClaimTypes.Surname)?.Value ?? null;

            var signin = _signInManager.ExternalLoginSignInAsync("Google", loginInfo.ProviderKey
                , false, true).Result;
            if (signin.Succeeded)
            {
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect("~/");

                }
                return RedirectToAction("Index", "Home");
            }
            else if (signin.RequiresTwoFactor)
            {
                //
            }

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    FirstName = FirstName,
                    LastName = LastName,
                    EmailConfirmed = true,
                };
                var resultAdduser = _userManager.CreateAsync(newUser).Result;
                user = newUser;
            }
            var resultAddlogin = _userManager.AddLoginAsync(user, loginInfo).Result;
            _signInManager.SignInAsync(user, false).Wait();


            return Redirect("/");
        }

        private void TransferBasketForuser(string userId)
        {
            string cookieName = "CartId";
            if (Request.Cookies.ContainsKey(cookieName))
            {
                var anonymousId = Request.Cookies[cookieName];
                _cart.TransferBasket(anonymousId, userId);
                Response.Cookies.Delete(cookieName);
            }
        }
    }
}
