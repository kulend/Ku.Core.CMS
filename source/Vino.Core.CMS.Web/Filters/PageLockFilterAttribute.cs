using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Filters
{
    public class PageLockFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var code = context.HttpContext.Session.GetString($"page_lock");
            //if ()
            //{ }
            //context.Result = new RedirectResult("/Account/Lock");
            base.OnActionExecuting(context);
        }
    }
}
