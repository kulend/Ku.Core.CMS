using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Web.Backend.Models;
using Vino.Core.Tokens.Jwt;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.Cache;
using Microsoft.Extensions.Options;
using Vino.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Vino.Core.CMS.Domain;

namespace Vino.Core.CMS.Web.Admin.Views.Account
{
    public class AccountController : BaseController
    {
        private IUserService userService;
        private IJwtProvider _jwtProvider;
        private JwtSecKey _jwtSecKey;
        private JwtAuthConfig _jwtAuthConfig;
        private ICacheService cacheService;

        public AccountController(IJwtProvider jwtProvider,
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

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

        /// <summary>
        /// 修改密码
        /// </summary>
        [Auth]
        public IActionResult PasswordEdit()
        {
            return View();
        }

        /// <summary>
        /// 保存密码
        /// </summary>
        [Auth]
        [UserAction("修改密码")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePassword(PwdData data)
        {
            if (!data.NewPassword.Equals(data.ConfirmPassword))
            {
                throw new VinoArgNullException("两次输入的密码不一致！");
            }
            await userService.ChangePassword(base.User.GetUserIdOrZero(), data.CurrentPassword, data.NewPassword);

            await HttpContext.Authentication.SignOutAsync("Cookie");

            return Json(true);
        }
    }
}
