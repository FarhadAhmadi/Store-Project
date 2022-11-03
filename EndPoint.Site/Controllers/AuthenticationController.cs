using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Store.Aplication.Services.Users.Commands.RegisterUser;
using Store.Common.Dto;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Bugeto_Store.Application.Services.Users.Commands.UserLogin;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _RegisterUserService;
        private readonly IUserLoginService _UserLoginService;
        public AuthenticationController(IRegisterUserService registerUserService , IUserLoginService userLoginService)
        {
            _RegisterUserService = registerUserService;
            _UserLoginService = userLoginService;
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string fullName , string email , string password , string rePassword)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(rePassword))
            {
                return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }

            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (password != rePassword)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (password.Length < 8)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }
            var signeupResult = _RegisterUserService.Execute(new RequestRegisterUserServiceDTO
            {
                Email = email,
                FullName = fullName,
                Password = password,
                RePassword = rePassword,
                Roles = new List<RoleDTO>
                {
                    new RoleDTO
                    {
                        Id = 3
                    }
                }
            });

            if (signeupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, "Customer"),
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(signeupResult);
        }
        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var signupResult = _UserLoginService.Execute(Email, Password);
            if (signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name),

            };
                foreach (var item in signupResult.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(signupResult);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}