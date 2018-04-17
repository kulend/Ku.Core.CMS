using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vino.Core.CMS.Web.Security;

namespace Vino.Core.CMS.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// 获取jwt 中的 jti，相当于每个token的唯一id
        /// </summary>
        public static string GetTokenIdOrNull(this ClaimsPrincipal self)
        {
            var value = self.FindFirst(ClaimTypes.Hash)?.Value;
            return value;
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

        /// <summary>
        /// 获取头像
        /// </summary>
        public static string GetHeadImage(this ClaimsPrincipal self)
        {
            var value = self.FindFirst("HeadImage")?.Value;
            return value??"";
        }

        /// <summary>
        /// 获取会员身份Identity
        /// </summary>
        public static MemberRole? GetMemberRole(this ClaimsPrincipal self)
        {
            var value = self.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return (MemberRole)int.Parse(value);
        }

        public static string GetVersion(this ClaimsPrincipal self)
        {
            var version = self.FindFirst(ClaimTypes.Version)?.Value;
            return version;
        }
    }
}
