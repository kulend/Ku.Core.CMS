using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.Web.Filters
{
    public abstract class VinoActionAttribute : Attribute
    {
        public IIocResolver IocResolver { set; get; }

        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public virtual void OnActionExecuted(ActionExecutedContext context)
        { 
        }
    }
}
