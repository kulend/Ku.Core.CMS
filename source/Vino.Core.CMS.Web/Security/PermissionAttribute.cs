using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Security
{
    public enum PermissionType
    {
        /// <summary>
        /// 仅检测用户是否授权
        /// </summary>
        None = 0,
        /// <summary>
        /// 页面
        /// </summary>
        Page = 1,
        /// <summary>
        /// 操作
        /// </summary>
        Action = 2
    }

    public class PermissionDefaultCodeHelper
    {
        /// <summary>
        /// 添加
        /// </summary>
        public const string Add = "add";
        /// <summary>
        /// 编辑
        /// </summary>
        public const string Edit = "edit";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = "delete";
        /// <summary>
        /// 查看
        /// </summary>
        public const string View = "view";
        /// <summary>
        /// 保存
        /// </summary>
        public const string Save = "save";
    }

    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionType PermissionType { get; set; }

        /// <summary>
        /// 检测URL
        /// 当permissionType 为Page时：
        /// 如果此值为空，则检测当前请求的URL （controllerName + actionName）
        /// 当permissionType为Code时：
        /// 如果此值为空，则默认检测url为（当前请求的ControllerName + actionName）
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        public PermissionAttribute()
        {
            this.PermissionType = PermissionType.Page;
        }

        public PermissionAttribute(PermissionType type) : this(type, null, null)
        {

        }

        public PermissionAttribute(string code) : this(PermissionType.Action, null, code)
        {

        }

        public PermissionAttribute(PermissionType type, string code) : this(type, null, code)
        {

        }

        public PermissionAttribute(PermissionType type, string url, string code) : base("permission")
        {
            this.PermissionType = type;
            this.Url = url;
            this.Code = code;
        }
    }
}
