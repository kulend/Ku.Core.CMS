using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Extensions.DbMigration;

namespace Ku.Core.CMS.Web.Filters
{
    public class JsonWrapperResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is JsonResult jsonResult
                && !(context.Result is OriginJsonResult) 
                && !(context.Result is DbMigrationJsonResult))
            {
                var oldValue = jsonResult.Value;
                jsonResult.Value = new
                {
                    code = 0,
                    data = oldValue
                };
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
