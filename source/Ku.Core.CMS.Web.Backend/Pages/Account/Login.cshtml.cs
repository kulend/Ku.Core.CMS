﻿using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Backend.Models;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Configs;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.EventBus;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.Account
{
    public class LoginModel : BasePage
    {
        private IUserService _userService;
        private IJwtProvider _jwtProvider;
        private JwtAuthConfig _jwtAuthConfig;
        private ICacheProvider _cacheService;
        private readonly IEventPublisher _eventPublisher;

        public LoginModel(IJwtProvider jwtProvider,
            IOptions<JwtAuthConfig> jwtAuthConfig,
            ICacheProvider _cacheService,
            IUserService _userService,
            IEventPublisher _eventPublisher)
        {
            this._jwtProvider = jwtProvider;
            this._jwtAuthConfig = jwtAuthConfig.Value;
            this._cacheService = _cacheService;
            this._userService = _userService;
            this._eventPublisher = _eventPublisher;
        }

        [BindProperty]
        public LoginData Input { set; get; }

        [IgnorePageLock]
        public void OnGet()
        {
            Input = new LoginData();
#if DEBUG
            Input.Account = "admin";
            Input.Password = "123456";
#endif
        }

        [IgnorePageLock]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new KuArgNullException();
            }

            //图片验证码
#if !DEBUG
            if(Input.ImageCode.IsNullOrEmpty())
            {
                throw new KuException("请输入验证码!");
            }
#endif
            if (!Input.ImageCode.IsNullOrEmpty())
            {
                var code = HttpContext.Session.GetString($"ImageValidateCode_login");
                HttpContext.Session.Remove("ImageValidateCode_login");
                if (!Input.ImageCode.EqualOrdinalIgnoreCase(code))
                {
                    throw new KuException(1, "验证码出错!");
                }
            }
            var user = await _userService.LoginAsync(Input.Account, Input.Password);
            if (user == null)
            {
                throw new KuException("登陆出错!");
            }
            //UserActionLogDto log = new UserActionLogDto();
            //log.Operation = "用户登陆";
            //log.ControllerName = "Home";
            //log.ActionName = "Login";
            //log.UserId = user.Id;
            //log.Ip = HttpContext.IpAddress();
            //log.Url = HttpContext.RequestPath();
            //log.UrlReferrer = HttpContext.UrlReferrer();
            //log.UserAgent = HttpContext.UserAgent().Substr(0, 250);
            //log.Method = HttpContext.Request.Method;
            //log.QueryString = HttpContext.Request.QueryString.ToString().Substr(0, 250);
            //log.CreateTime = DateTime.Now;

            //await _eventPublisher.PublishAsync(log);

            var claims = new List<Claim>()
            {
                new Claim("Account",user.Account)
                ,new Claim(ClaimTypes.Name,user.NickName)
                ,new Claim("HeadImage", user.HeadImage??"")
                ,new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = _jwtProvider.CreateToken(claims);

            base.Response.Cookies.Append(_jwtAuthConfig.CookieName, token, new CookieOptions
            {
                HttpOnly = true
            });

            //清除用户权限缓存
            await _cacheService.RemoveAsync(string.Format(CacheKeyDefinition.UserAuthCode, user.Id));
            await _cacheService.RemoveAsync(string.Format(CacheKeyDefinition.UserAuthCodeEncrypt, user.Id));

            //Cookie中保存用户信息
            base.Response.Cookies.Append("user.name", user.NickName);
            base.Response.Cookies.Append("user.headimage", user.HeadImage ?? "");

            return JsonData(true);
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [IgnorePageLock]
        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Account/Login");
        }
    }
}