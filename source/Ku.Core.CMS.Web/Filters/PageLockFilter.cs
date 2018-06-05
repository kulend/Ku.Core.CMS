using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Reflection;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Web.Extensions;

namespace Ku.Core.CMS.Web.Filters
{
    /// <summary>
    /// 处理页面锁定
    /// </summary>
    public class PageLockFilter : IActionFilter, IPageFilter
    {
        private readonly ICacheProvider _cacheService;

        public PageLockFilter(ICacheProvider cacheService)
        {
            this._cacheService = cacheService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }
            MethodInfo method = context.GetHandlerMethod();
            if (method == null || method.CustomAttributes.Any(x => x.AttributeType == typeof(IgnorePageLockAttribute)))
            {
                return;
            }

            var tokenId = context.HttpContext.User.GetTokenIdOrNull();
            var isLock = _cacheService.Get<bool>(string.Format(CacheKeyDefinition.PageLock, tokenId));
            if (isLock)
            {
                if (context.HttpContext.Request.IsJsonRequest())
                {
                    context.Result = new JsonResult(new
                    {
                        code = 905,
                        message = "页面已锁定"
                    });
                }
                else
                {
                    var url = context.HttpContext.Request.Path.Value;
                    context.Result = new RedirectResult("/Account/Lock?ReturnUrl=" + WebUtility.UrlEncode(url));
                }
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }
            MethodInfo method = context.GetHandlerMethod();
            if (method == null || method.CustomAttributes.Any(x => x.AttributeType == typeof(IgnorePageLockAttribute)))
            {
                return;
            }

            var tokenId = context.HttpContext.User.GetTokenIdOrNull();
            var isLock = _cacheService.Get<bool>(string.Format(CacheKeyDefinition.PageLock, tokenId));
            if (isLock)
            {
                if (context.HttpContext.Request.IsJsonRequest())
                {
                    context.Result = new JsonResult(new
                    {
                        code = 905,
                        message = "页面已锁定"
                    });
                }
                else
                {
                    var url = context.HttpContext.Request.Path.Value;
                    context.Result = new RedirectResult("/Account/Lock?ReturnUrl=" + WebUtility.UrlEncode(url));
                }
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
