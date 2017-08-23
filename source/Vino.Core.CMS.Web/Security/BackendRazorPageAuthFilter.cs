using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vino.Core.CMS.Web.Security
{
    /// <summary>
    /// Razor页面方法权限认证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BackendRazorPageAuthFilter : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            var authAttrs = GetAttributes(context.HandlerMethod.MethodInfo);
            if (authAttrs != null && authAttrs.Any())
            {
                //context
                context.HandlerMethod = null;
                context.HttpContext.Response.Redirect("/AccessDenied");
            }
        }

        private static IEnumerable<AuthAttribute> GetAttributes(MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttributes(typeof(AuthAttribute), false).Cast<AuthAttribute>();
        }
    }
}
