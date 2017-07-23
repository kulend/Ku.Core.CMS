using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vino.Core.CMS.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// 获取jwt 中的 jti，相当于每个token的唯一id
        /// </summary>
        public static string GetTokenIdOrNull(this ClaimsPrincipal self)
        {
            var jti = self.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            return jti;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        public static string GetUserNameOrNull(this ClaimsPrincipal self)
        {
            var userName = self.FindFirst(ClaimTypes.Name)?.Value;
            return userName;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        public static long GetUserIdOrZero(this ClaimsPrincipal self)
        {
            var id = self.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long.TryParse(id, out long result);
            return result;
        }
    }
}
