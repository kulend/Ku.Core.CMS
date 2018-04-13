using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Extensions;

namespace Vino.Core.CMS.Web.Security
{
    public class WebApiAuthAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class WebApiAuthAuthorizationHandler : DefaultAttributeAuthorizationHandler<WebApiAuthAuthorizationRequirement, MemberAuthAttribute>
    {
        private IFunctionService service;

        public WebApiAuthAuthorizationHandler(IFunctionService _service)
        {
            this.service = _service;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WebApiAuthAuthorizationRequirement requirement, List<MemberAuthAttribute> attributes, List<MemberAuthAttribute> controllAttributes)
        {
            var attr1 = controllAttributes.FirstOrDefault();
            if (!CheckPermissionAsync(context, attr1))
            {
                context.Fail();
                return;
            }
            var attr2 = attributes.FirstOrDefault();
            if (!CheckPermissionAsync(context, attr2))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }

        private bool CheckPermissionAsync(AuthorizationHandlerContext context, MemberAuthAttribute attribute)
        {
            if (attribute == null)
            {
                return true;
            }
            if (context.User == null)
            {
                return false;
            }
            var role = context.User.GetMemberRole();
            if (!role.HasValue) return false;
            return (attribute.Role & role.Value) != 0;
        }
    }
}
