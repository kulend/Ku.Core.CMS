using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Admin.Models;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.Tokens.Jwt;

namespace Vino.Core.CMS.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private IIocResolver _ioc;
        private IJwtProvider _jwtProvider;
        private JwtSecKey _jwtSecKey;
        private JwtAuthConfig _jwtAuthConfig;

        public HomeController(IJwtProvider jwtProvider,
            IOptions<JwtSecKey> jwtSecKey,
            IOptions<JwtAuthConfig> jwtAuthConfig,
            IIocResolver ioc)
        {
            _jwtProvider = jwtProvider;
            _jwtSecKey = jwtSecKey.Value;
            _jwtAuthConfig = jwtAuthConfig.Value;
            this._ioc = ioc;
        }

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
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginData data)
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
                HttpContext.Session.Remove("ImageValidateCode_login");
                if (!data.ImageCode.EqualOrdinalIgnoreCase(code))
                {
                    throw new VinoException(1, "验证码出错!");
                }
            }

            var service = _ioc.Resolve<ILoginService>();
            var user = service.DoLogin(data.Account, data.Password);

            var claims = new List<Claim>()
            {
                new Claim("Account",user.Account)
                ,new Claim(ClaimTypes.Name,user.Name)
                ,new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = _jwtProvider.CreateToken(_jwtSecKey.Key, _jwtAuthConfig.Issuer, _jwtAuthConfig.Audience, claims,
                DateTime.Now.AddMinutes(_jwtSecKey.ExpiredMinutes));
            base.Response.Cookies.Append(_jwtAuthConfig.CookieName, token, new CookieOptions
            {
                HttpOnly = true
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
