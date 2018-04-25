using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.Web.Filters
{
    public class VinoActionFilter : IActionFilter, IAsyncActionFilter, IPageFilter
    {
        public IIocResolver _iocResolver;

        public IEnumerable<VinoActionAttribute> _attrs;

        public VinoActionFilter(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                attr.OnActionExecuted(context);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                attr.OnActionExecuting(context);
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                await attr.OnActionExecutionAsync(context);
            }

            await next();
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                attr.OnPageHandlerExecuted(context);
            }
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                attr.OnPageHandlerExecuting(context);
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            var attrs = GetAttrs(context);
            foreach (var attr in attrs)
            {
                attr.OnPageHandlerSelected(context);
            }
        }

        private IEnumerable<VinoActionAttribute> GetAttrs(FilterContext context)
        {
            if (_attrs != null)
            {
                return _attrs;
            }

            var method = context.GetHandlerMethod();
            if (method == null)
            {
                return new VinoActionAttribute[] { };
            }

            _attrs = method.GetCustomAttributes<VinoActionAttribute>(true);
            foreach (var attr in _attrs)
            {
                if (attr.IocResolver == null)
                {
                    attr.IocResolver = _iocResolver;
                }
            }
            return _attrs;
        }
    }
}
