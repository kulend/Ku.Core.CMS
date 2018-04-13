using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Tokens.Jwt;

namespace Vino.Core.CMS.WebApi.Controllers.V1.Account
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AccountController : WebApiController
    {
        private IJwtProvider _jwtProvider;

        public AccountController(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        [HttpGet("login")]
        [HttpPost("login")]
        public async Task<JsonResult> Login(string mobile, string password)
        {
            //验证
            var claims = new List<Claim>()
            {
                new Claim("Account", mobile)
                ,new Claim(ClaimTypes.Name, mobile)
                ,new Claim(ClaimTypes.NameIdentifier, "11111111111")
                ,new Claim(ClaimTypes.Role, ((int)MemberRole.Default).ToString())
            };

            var token = _jwtProvider.CreateToken(claims);

            return Json(token);
        }
    }
}
