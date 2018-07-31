using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Extensions;
using Dnc.Api.Throttle;

namespace Ku.Core.CMS.Web.Filters
{
    public class WebApiResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is FileContentResult file)
            {
                await next();
                return;
            }

            if (context.Result is ApiThrottleResult)
            {
                var rst = new JsonResult(new
                {
                    code = 906,
                    message = "访问过于频繁，请稍后重试"

                });
                context.Result = rst;

                await next();
                return;
            }

            object value = null;
            if (context.Result is ObjectResult obj)
            {
                value = obj.Value;
            } else if (context.Result is JsonResult json)
            {
                value = json.Value;
            }
            
            var result = new JsonResult(new {
                code = 0,
                data = value
            });
            context.Result = result;

            await next();
        }
    }
}
