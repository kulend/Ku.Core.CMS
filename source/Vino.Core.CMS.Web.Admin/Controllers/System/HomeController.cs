using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Admin.Models;
using Vino.Core.CMS.Web.Base;

namespace Vino.Core.CMS.Web.Admin.Controllers.System
{
    public class HomeController : BaseController
    {
        [Authorize]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewData["LoginUserName"] = identity.FindFirst(ClaimTypes.Name).Value;
            return View();
        }

        public IActionResult Login()
        {
            var url = Request.Query["ReturnUrl"];
            ViewData["ReturnUrl"] = string.IsNullOrEmpty(url) ? "/" : url.ToString();
            LoginData data = new LoginData();
            data.Account = "admin";
            data.Password = "123456";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginData data)
        {
            //图片验证码
#if !DEBUG
            if(data.ImageCode.IsNullOrEmpty())
            {
                throw new VinoException("请输入验证码!");
            }
#endif
            if (!data.ImageCode.IsNullOrEmpty())
            {
                var code = HttpContext.Session.GetString($"ImageValidateCode_login");
                if (!data.ImageCode.EqualOrdinalIgnoreCase(code))
                {
                    throw new VinoException(1, "验证码出错!");
                }
            }

            var service = IoC.Resolve<ILoginService>();
            var user = service.DoLogin(data.Account, data.Password);

            //you can add all of ClaimTypes in this collection 
            var claims = new List<Claim>()
            {
                new Claim("Account",user.Account)
                ,new Claim(ClaimTypes.Name,user.Name)
                ,new Claim("ID", user.Id.ToString())
                //,new Claim(ClaimTypes.Email,"emailaccount@microsoft.com") 
            };

            //init the identity instances 
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

            //signin
            await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                IsPersistent = false,
                AllowRefresh = false
            });

            return JsonData(true);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToAction("Index", "Home");
        }
    }
}
