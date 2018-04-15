using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Domain;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Filters
{
    /// <summary>
    /// 该特性指示需要图像验证码验证
    /// </summary>
    public class NeedVerifyCodeAttribute : VinoActionAttribute
    {
        /// <summary>
        /// 需要验证码
        /// </summary>
        public bool IsNeedVerifyCode { get; set; }

        public NeedVerifyCodeAttribute() : this(true)
        {
        }

        public NeedVerifyCodeAttribute(bool needVCode)
        {
            this.IsNeedVerifyCode = needVCode;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsNeedVerifyCode)
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
                ICacheService _cacheService = IocResolver.Resolve<ICacheService>();
                var cacheKey = string.Format(CacheKeyDefinition.VerifyCode, key);
                var checkCode = _cacheService.Get<string>(cacheKey);
                if (!code.Equals(checkCode, StringComparison.OrdinalIgnoreCase))
                {
                    _cacheService.Remove(cacheKey);
                    throw new VinoVerifyCodeInvalidException("验证码不正确");
                }
                _cacheService.Remove(cacheKey);
            }
            base.OnActionExecuting(context);
        }
    }
}
