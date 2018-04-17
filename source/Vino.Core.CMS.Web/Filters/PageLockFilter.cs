using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Vino.Core.Cache;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Web.Extensions;
using System.Linq;
using System.Net;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Filters
{
    /// <summary>
    /// 处理页面锁定
    /// </summary>
    public class PageLockFilter : IResourceFilter
    {
        private readonly ICacheService _cacheService;

        public PageLockFilter(ICacheService cacheService)
        {
            this._cacheService = cacheService;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            if (context.HttpContext.User.Identity.IsAuthenticated 
                && !action.MethodInfo.CustomAttributes.Any(x=>x.AttributeType == typeof(IgnorePageLockAttribute)))
            {
                var tokenId = context.HttpContext.User.GetTokenIdOrNull();
                var isLock = _cacheService.Get<bool>(string.Format(CacheKeyDefinition.PageLock, tokenId));
                if (isLock)
                {
                    if (context.HttpContext.Request.IsJsonRequest())
                    {
                        context.Result = new JsonResult(new {
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
        }
    }
}
