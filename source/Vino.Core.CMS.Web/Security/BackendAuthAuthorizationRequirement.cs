using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.Infrastructure.Extensions;

namespace Vino.Core.CMS.Web.Security
{
    public class BackendAuthAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class BackendAuthAuthorizationHandler : BackendAttributeAuthorizationHandler<AuthAuthorizationRequirement, AuthAttribute>
    {
        private IFunctionService service;

        public BackendAuthAuthorizationHandler(IFunctionService _service)
        {
            this.service = _service;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthAuthorizationRequirement requirement, List<AuthAttribute> attributes, List<AuthAttribute> controllAttributes)
        {
            //var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            //if (action == null)
            //{
            //    context.Succeed(requirement);
            //    return;
            //}
            string baseCode = controllAttributes.FirstOrDefault()?.AuthCode;
            foreach (var attribute in controllAttributes)
            {
                //检测权限
                if (!await CheckPermissionAsync(context, attribute))
                {
                    //throw new VTException(VTExceptionType.NoPermission, "您无权操作此功能");
                    context.Fail();
                    return;
                }
            }
            foreach (var attribute in attributes)
            {
                //检测权限
                if (!await CheckPermissionAsync(context, attribute, baseCode))
                {
                    //throw new VTException(VTExceptionType.NoPermission, "您无权操作此功能");
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }

        private async Task<bool> CheckPermissionAsync(AuthorizationHandlerContext context, AuthAttribute attribute,
            string baseCode = null)
        {
            if (attribute == null)
            {
                return true;
            }
            if (attribute.AuthCode.IsNullOrEmpty())
            {
                return context.User != null;
            }
            var userId = context.User.GetUserIdOrZero();
            if (userId == 0)
            {
                return false;
            }
            return await service.CheckUserAuth(userId,
                (baseCode.IsNullOrEmpty() || attribute.IsFullAuthCode) ? attribute.AuthCode : $"{baseCode}.{attribute.AuthCode}");
        }
    }
}
