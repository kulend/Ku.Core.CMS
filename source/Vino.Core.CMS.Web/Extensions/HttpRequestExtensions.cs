using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Vino.Core.CMS.Web.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 是否是返回json请求
        /// </summary>
        public static bool IsJsonRequest(this HttpRequest request)
        {
            return request.Headers != null &&
                   (request.Headers["X-Requested-With"] == "XMLHttpRequest"
                    || request.Headers["Accept"].Contains("application/json"));
        }
    }
}
