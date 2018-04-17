using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.EventBus;
using Vino.Core.CMS.IService.System;
using System.Net;

namespace Vino.Core.CMS.Web.Admin.Views.Account
{
    /// <summary>
    /// 账户处理
    /// </summary>
    public class AccountController : BackendController
    {
        private IUserService userService;
        private IJwtProvider _jwtProvider;
        private JwtAuthConfig _jwtAuthConfig;
        private ICacheService cacheService;
        private readonly IEventPublisher _eventPublisher;

        public AccountController(IJwtProvider jwtProvider,
            IOptions<JwtAuthConfig> jwtAuthConfig,
            ICacheService _cacheService,
            IUserService _userService,
            IEventPublisher _eventPublisher)
        {
            _jwtProvider = jwtProvider;
            _jwtAuthConfig = jwtAuthConfig.Value;
            this.cacheService = _cacheService;
            this.userService = _userService;
            this._eventPublisher = _eventPublisher;
        }

        [IgnorePageLock]
        public IActionResult Login()
        {
            var url = Request.Query["ReturnUrl"];
            ViewData["ReturnUrl"] = string.IsNullOrEmpty(url) ? "/" : url.ToString();
            LoginData data = new LoginData();
#if DEBUG
            data.Account = "admin";
            data.Password = "123456";
#endif
            return View(data);
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
            UserActionLogDto log = new UserActionLogDto();
            log.Operation = "用户登陆";
            log.ControllerName = "Home";
            log.ActionName = "Login";
            log.UserId = user.Id;
            log.Ip = HttpContext.IpAddress();
            log.Url = HttpContext.RequestPath();
            log.UrlReferrer = HttpContext.UrlReferrer();
            log.UserAgent = HttpContext.UserAgent().Substr(0, 250);
            log.Method = HttpContext.Request.Method;
            log.QueryString = HttpContext.Request.QueryString.ToString().Substr(0, 250);
            log.CreateTime = DateTime.Now;

            await _eventPublisher.PublishAsync(log);

            var claims = new List<Claim>()
            {
                new Claim("Account",user.Account)
                ,new Claim(ClaimTypes.Name,user.Name)
                ,new Claim("HeadImage", user.HeadImage??"")
                ,new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = _jwtProvider.CreateToken(claims);

            base.Response.Cookies.Append(_jwtAuthConfig.CookieName, token, new CookieOptions
            {
                HttpOnly = true
            });

            //清除用户权限缓存
            cacheService.Remove(string.Format(CacheKeyDefinition.UserAuthCode, user.Id));
            cacheService.Remove(string.Format(CacheKeyDefinition.UserAuthCodeEncrypt, user.Id));

            //Cookie中保存用户信息
            base.Response.Cookies.Append("user.name", user.Name);
            base.Response.Cookies.Append("user.headimage", user.HeadImage ?? "");

            return Json(true);
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [IgnorePageLock]
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

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Json(true);
        }

        /// <summary>
        /// 页面加锁
        /// </summary>
        [Auth]
        public async Task<IActionResult> PageLock()
        {
            //缓存中添加页面锁定消息
            var tokenId = User.GetTokenIdOrNull();
            cacheService.Add(string.Format(CacheKeyDefinition.PageLock, tokenId), true, TimeSpan.FromMinutes(_jwtProvider.GetJwtConfig().ExpiredMinutes));
            return Json(true);
        }

        /// <summary>
        /// 页面解锁
        /// </summary>
        [Auth]
        [IgnorePageLock]
        public async Task<IActionResult> PageUnlock([FromForm]string Password)
        {
            var userId = User.GetUserIdOrZero();
            if (!await userService.PasswordCheckAsync(userId, Password))
            {
                throw new VinoException("解锁密码不正确！");
            }

            //缓存中去除页面锁定消息
            var tokenId = User.GetTokenIdOrNull();
            cacheService.Remove(string.Format(CacheKeyDefinition.PageLock, tokenId));

            return Json(true);
        }

        /// <summary>
        /// 锁定页面
        /// </summary>
        /// <returns></returns>
        [Auth]
        [IgnorePageLock]
        public async Task<IActionResult> Lock()
        {
            ViewBag.ReturnUrl = WebUtility.UrlDecode(Request.Query["ReturnUrl"]);
            return View();
        }
    }
}
