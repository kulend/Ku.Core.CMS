using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Ku.Core.CMS.Web.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;

        public ApiExceptionFilter(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<ApiExceptionFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var code = 500;
            var message = exception.Message;

            _logger.LogError(new EventId(code), exception, message);

            if (exception is VinoException ex)
            {
                handleVinoException(context, ex);
            }
            else
            {
                handleVinoException(context, new VinoException("有错误发生！"));
            }
        }

        private void handleVinoException(ExceptionContext context, VinoException ex)
        {
            context.Result = new JsonResult(new
            {
                code = ex.Code,
                message = ex.Message,
                data = ex.Data
            });
            context.Exception = null;
        }
    }
}
