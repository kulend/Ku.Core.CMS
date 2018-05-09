using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static string IpAddress(this HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Connection.RemoteIpAddress.ToString();
        }

        public static string RequestPath(this HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Request.Path;
        }

        public static string UrlReferrer(this HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Request.Headers[HeaderNames.Referer].ToString();
        }

        public static string UserAgent(this HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Request.Headers[HeaderNames.UserAgent].ToString();
        }
    }
}
