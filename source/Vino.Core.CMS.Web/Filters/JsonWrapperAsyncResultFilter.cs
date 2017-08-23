using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vino.Core.CMS.Web.Base;

namespace Vino.Core.CMS.Web.Filters
{
    public class JsonWrapperAsyncResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is JsonResult jsonResult 
                && !(context.Result is LayuiJsonResult))
            {
                var oldValue = jsonResult.Value;
                jsonResult.Value = new
                {
                    code = 0,
                    data = oldValue
                };
            }

            await next();
        }
    }
}
