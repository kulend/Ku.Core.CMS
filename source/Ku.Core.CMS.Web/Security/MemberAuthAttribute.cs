using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ku.Core.CMS.Web.Security
{
    public class MemberAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 会员角色
        /// </summary>
        public MemberRole Role { get; set; }

        public MemberAuthAttribute() : this(MemberRole.Default)
        {
        }

        public MemberAuthAttribute(MemberRole Role) : base("auth")
        {
            this.Role = Role;
        }
    }

    [System.Flags]
    public enum MemberRole
    {
        Default = 1,
        Teacher = 2,
        Other = 4
    }
}
