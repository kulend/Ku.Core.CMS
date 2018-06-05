using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Filters
{
    /// <summary>
    /// 该特性指示需要图像验证码验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NeedVerifyCodeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 图像验证码验证
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Query.ContainsKey("__vcode")
                || !context.HttpContext.Request.Query.ContainsKey("__vkey"))
            {
                throw new VinoNeedVerifyCodeException();
            }

            var code = context.HttpContext.Request.Query["__vcode"].FirstOrDefault();
            var key = context.HttpContext.Request.Query["__vkey"].FirstOrDefault();
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(key))
            {
                throw new VinoVerifyCodeInvalidException("验证码不能为空");
            }

            //缓存中取得验证码
            ICacheProvider _cacheService = context.HttpContext.RequestServices.GetService<ICacheProvider>();
            var cacheKey = string.Format(CacheKeyDefinition.VerifyCode, key);
            var checkCode = _cacheService.Get<string>(cacheKey);
            if (!code.Equals(checkCode, StringComparison.OrdinalIgnoreCase))
            {
                _cacheService.Remove(cacheKey);
                throw new VinoVerifyCodeInvalidException("验证码不正确");
            }
            _cacheService.Remove(cacheKey);

            base.OnActionExecuting(context);
        }
    }
}