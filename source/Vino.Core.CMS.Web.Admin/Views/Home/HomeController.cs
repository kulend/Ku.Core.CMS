using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vino.Core.Cache;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Admin.Models;
using Vino.Core.Tokens.Jwt;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Domain;

namespace Vino.Core.CMS.Web.Admin.Views.Home
{
    public class HomeController : BaseController
    {
        private IJwtProvider _jwtProvider;
        private JwtSecKey _jwtSecKey;
        private JwtAuthConfig _jwtAuthConfig;
        private ICacheService cacheService;
        private IUserService userService;

        public HomeController(IJwtProvider jwtProvider,
            IOptions<JwtSecKey> jwtSecKey,
            IOptions<JwtAuthConfig> jwtAuthConfig,
            ICacheService _cacheService,
            IUserService _userService)
        {
            _jwtProvider = jwtProvider;
            _jwtSecKey = jwtSecKey.Value;
            _jwtAuthConfig = jwtAuthConfig.Value;
            this.cacheService = _cacheService;
            this.userService = _userService;
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
#if DEBUG
            data.Account = "admin";
            data.Password = "123456";
#endif
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                HttpContext.Session.Remove("ImageValidateCode_login");
                if (!data.ImageCode.EqualOrdinalIgnoreCase(code))
                {
                    throw new VinoException(1, "验证码出错!");
                }
            }
            var user = await userService.LoginAsync(data.Account, data.Password);
            if (user == null)
            {
                throw new VinoException("登陆出错!");
            }
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
            //清楚用户权限缓存
            cacheService.Remove(string.Format(CacheKeyDefinition.UserAuthCode, user.Id));
            cacheService.Remove(string.Format(CacheKeyDefinition.UserAuthCodeEncrypt, user.Id));
            return Json(true);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, HttpPost]
        public IActionResult AccessDenied()
        {
            if (base.IsJsonRequest())
            {
                throw new VinoAccessDeniedException();
            }

            return View();
        }
    }
}
