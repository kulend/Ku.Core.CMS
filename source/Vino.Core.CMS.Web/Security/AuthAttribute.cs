using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Security
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public bool IsFullAuthCode { get; set; } = false;

        /// <summary>
        /// 权限码
        /// </summary>
        public string AuthCode { get; set; }

        public AuthAttribute() : base("auth")
        {
        }

        public AuthAttribute(string authCode) : this(authCode, false)
        {
        }

        public AuthAttribute(string authCode, bool isFullAuthCode) : base("auth")
        {
            this.AuthCode = authCode;
            this.IsFullAuthCode = isFullAuthCode;
        }
    }
}
