using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ku.Core.CMS.Web.Security
{
    public class UserAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole Role { get; set; }

        public UserAuthAttribute() : this(UserRole.Default)
        {
        }

        public UserAuthAttribute(UserRole Role) : base("auth")
        {
            this.Role = Role;
        }
    }

    [System.Flags]
    public enum UserRole
    {
        Default = 1,
        Admin = 2,
        Other = 4
    }
}
