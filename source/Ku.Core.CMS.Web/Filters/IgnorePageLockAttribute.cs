using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Web.Filters
{
    /// <summary>
    /// 加上该特性表示当前请求不验证页面是否已锁定
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IgnorePageLockAttribute : Attribute
    {

    }
}
