using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.Web.Filters
{
    public class VinoActionFilter : IActionFilter
    {
        public IIocResolver _iocResolver;

        public VinoActionFilter(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            var attrs = action.MethodInfo.GetCustomAttributes<VinoActionAttribute>(true);
            foreach (var attr in attrs)
            {
                if (attr.IocResolver == null)
                {
                    attr.IocResolver = _iocResolver;
                }
                attr.OnActionExecuted(context);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            var attrs = action.MethodInfo.GetCustomAttributes<VinoActionAttribute>(true);
            foreach (var attr in attrs)
            {
                if (attr.IocResolver == null)
                {
                    attr.IocResolver = _iocResolver;
                }
                attr.OnActionExecuting(context);
            }
        }
    }
}
