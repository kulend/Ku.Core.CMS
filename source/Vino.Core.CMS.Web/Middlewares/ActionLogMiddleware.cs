using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Web.Middlewares
{
    public class ActionLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ActionLogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ActionLogMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Handling request: " + context.Request.Path);
            
            try
            {
                UserActionLog log = new UserActionLog();
                log.Url = context.Request.Path;
                log.Ip = context.Connection.RemoteIpAddress.ToString();
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                throw;
            }
            _logger.LogInformation("Finished handling request.");
        }
    }
}
