using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vino.Core.CMS.Web.Security
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        //private IAdminModuleService _adminModuleService;

        //public PermissionAuthorizationHandler(IAdminModuleService adminModuleService)
        //{
        //    this._adminModuleService = adminModuleService;
        //}

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
        {
            var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                context.Succeed(requirement);
                return;
            }
            foreach (var attribute in attributes)
            {
                //检测权限
                if (!await CheckPermissionAsync(context, action, attribute))
                {
                    //throw new VTException(VTExceptionType.NoPermission, "您无权操作此功能");
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }

        private async Task<bool> CheckPermissionAsync(AuthorizationHandlerContext context, ControllerActionDescriptor action, PermissionAttribute permissionAttr)
        {
            //var userId = context.User.GetUserIdOrZero();
            //var checkUrl = permissionAttr.Url;

            //if (permissionAttr.PermissionType == PermissionType.Page)
            //{
            //    if (string.IsNullOrEmpty(checkUrl))
            //    {
            //        checkUrl = $"{action.ControllerName}/{action.ActionName}";
            //    }
            //    return await this._adminModuleService.CheckModulePermissionByUrl(userId, checkUrl);
            //}
            //else if (permissionAttr.PermissionType == PermissionType.Action)
            //{
            //    if (string.IsNullOrEmpty(permissionAttr.Code))
            //    {
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(checkUrl))
            //    {
            //        checkUrl = $"{action.ControllerName}/";
            //    }

            //    return await this._adminModuleService.CheckModulePermissionByCode(userId, checkUrl, permissionAttr.Code, $"{action.ControllerName}/{action.ActionName}");
            //}

            return true;
        }
    }
}
