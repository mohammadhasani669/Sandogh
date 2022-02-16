using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sandogh.Admin.EndPoint.Attributes;
using Sandogh.Admin.EndPoint.Models;
using Sandogh.Admin.EndPoint.Models.VIewModels.Account;
using Sandogh.Domain.Users;
using System.ComponentModel;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

      
        public IActionResult Login(string returnUrl= "/")
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
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
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.RequiresTwoFactor)
            {

            }
            if (result.IsLockedOut)
            {

            }
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است");

            return View(model);
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return Redirect("/");
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
            if (ModelState.IsValid)
            {

                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber
                };
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    string callbackUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        UserId = user.Id,
                        token = token
                    }, protocol: Request.Scheme);

                    string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                    //_emailService.Execute(user.Email, body, "فعال سازی حساب کاربری باگتو");

                    return RedirectToAction("index","Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

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
            // _emailService.Execute(user.Email, body, "فراموشی رمز عبور");
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


    }
}
