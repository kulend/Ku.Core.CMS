using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Admin.Models.System;

namespace Vino.Core.CMS.Web.Admin.Controllers.System
{
    public class AccountController : BasePageController
    {
        [HttpGet]
        public IActionResult Login()
        {
            var url = Request.Query["ReturnUrl"];
            ViewData["ReturnUrl"] = string.IsNullOrEmpty(url) ? "/" : url.ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormData userFromFore)
        {
            var service = IoC.Resolve<ILoginService>();
            var user = service.DoLogin(userFromFore.Account, userFromFore.Password);

            //you can add all of ClaimTypes in this collection 
            var claims = new List<Claim>()
            {
                new Claim("Account",user.Account)
                ,new Claim(ClaimTypes.Name,user.OperatorName)
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

            return Json(new { code = 0 });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost,HttpGet]
        public JsonResult CreateAccount(string name,string pwd)
        {
            var service = IoC.Resolve<ILoginService>();
            var user = service.Create(name, pwd);
            return Json(new { code = 0, data= user });
        }
    }
}
