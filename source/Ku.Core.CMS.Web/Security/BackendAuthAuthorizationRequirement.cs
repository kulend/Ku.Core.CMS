using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Security
{
    public class BackendAuthAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class BackendAuthAuthorizationHandler : DefaultAttributeAuthorizationHandler<BackendAuthAuthorizationRequirement, AuthAttribute>
    {
        private IBackendAuthService service;

        public BackendAuthAuthorizationHandler(IBackendAuthService _service)
        {
            this.service = _service;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BackendAuthAuthorizationRequirement requirement, List<AuthAttribute> attributes, List<AuthAttribute> superAttributes)
        {
            //var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            //if (action == null)
            //{
            //    context.Succeed(requirement);
            //    return;
            //}
            string baseCode = superAttributes.FirstOrDefault()?.AuthCode;
            foreach (var attribute in superAttributes)
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
                return context.User != null && context.User.GetUserIdOrZero() != 0;
            }
            var userId = context.User.GetUserIdOrZero();
            if (userId == 0)
            {
                return false;
            }
            return await service.CheckUserAuthAsync(userId,
                (baseCode.IsNullOrEmpty() || attribute.IsFullAuthCode) ? attribute.AuthCode : $"{baseCode}.{attribute.AuthCode}");
        }
    }
}
