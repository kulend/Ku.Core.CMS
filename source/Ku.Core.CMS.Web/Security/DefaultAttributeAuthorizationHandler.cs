using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Security
{
    public abstract class DefaultAttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attrs = new List<TAttribute>();
            var superAttrs = new List<TAttribute>();
            var descriptor = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor;

            //RazorPages
            if (descriptor is CompiledPageActionDescriptor razorPagesDescriptor)
            {
                superAttrs.AddRange(GetAttributes(razorPagesDescriptor.HandlerTypeInfo));
                attrs.AddRange(GetAttributes(razorPagesDescriptor.HandlerMethods.FirstOrDefault()?.MethodInfo));
            }else if (descriptor is ControllerActionDescriptor controllerDescriptor)
            {
                //兼容MVC Controller
                superAttrs.AddRange(GetAttributes(controllerDescriptor.ControllerTypeInfo));
                attrs.AddRange(GetAttributes(controllerDescriptor.MethodInfo));
            }

            return HandleRequirementAsync(context, requirement, attrs, superAttrs);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, List<TAttribute> attrs, List<TAttribute> superAttrs);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }
}
