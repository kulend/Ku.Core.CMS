using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Configs;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.Tokens.Jwt;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.IService.UserCenter;

namespace Ku.Core.CMS.WebApi.Controllers.V1.Account
{
    /// <summary>
    /// 账户相关接口
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AccountController : WebApiController
    {
        private IJwtProvider _jwtProvider;
        private JwtConfig _jwtConfig;
        private ICacheProvider _cacheService;
        private readonly IUserService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountController(IJwtProvider jwtProvider, ICacheProvider cacheService, IOptions<JwtConfig> jwtConfig, IUserService service)
        {
            _jwtProvider = jwtProvider;
            _cacheService = cacheService;
            _service = service;
            _jwtConfig = jwtConfig.Value;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet("login")]
        [HttpPost("login")]
        [NeedVerifyCode]
        public async Task<JsonResult> Login(string mobile, string password)
        {
            //取得用户信息
            var user = await _service.MobileLoginAsync(mobile, password);

            //生成Token
            var tokenVersion = DateTime.Now.Ticks.ToString();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Version, tokenVersion)
                ,new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                ,new Claim(ClaimTypes.Name, user.NickName)
                ,new Claim(ClaimTypes.Role, ((int)UserRole.Default).ToString())
            };

            var token = _jwtProvider.CreateToken(claims);

            var loginMember = new LoginMember {
                Id = user.Id,
                Name = user.NickName
            };

            //如果当前已登陆，则退出当前登录
            await DoLogoutAsync();

            await _cacheService.SetAsync(string.Format(CacheKeyDefinition.ApiUserToken, user.Id, tokenVersion), loginMember, TimeSpan.FromMinutes(_jwtConfig.ExpiredMinutes));

            return Json(token);
        }

        //退出登录
        private async Task DoLogoutAsync()
        {
            if (User != null && !string.IsNullOrEmpty(User.GetTokenIdOrNull()))
            {
                await _cacheService.SetAsync(string.Format(CacheKeyDefinition.ApiExpiredToken, User.GetTokenIdOrNull()),"", TimeSpan.FromMinutes(_jwtConfig.ExpiredMinutes));
                await _cacheService.RemoveAsync(string.Format(CacheKeyDefinition.ApiUserToken, User.GetUserIdOrZero(), User.GetVersion()));
            }
        }
    }
}
