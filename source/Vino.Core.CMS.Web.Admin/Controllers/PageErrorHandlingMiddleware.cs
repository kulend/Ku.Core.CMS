using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Vino.Core.CMS.Core.Exceptions;

namespace Vino.Core.CMS.Web.Admin.Controllers
{
    public class PageErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ExceptionHandlerOptions _options;

        public PageErrorHandlingMiddleware(RequestDelegate next, IOptions<ExceptionHandlerOptions> options)
        {
            this._next = next;
            this._options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (VinoException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, VinoException ex)
        {
            var data = new { code = ex.Code, message = ex.Message, data = ex.Data };
            var result = JsonConvert.SerializeObject(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UsePageErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PageErrorHandlingMiddleware>();
        }
    }
}
