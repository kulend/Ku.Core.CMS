using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Web.Extensions;

namespace Vino.Core.CMS.Web.Filters
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
