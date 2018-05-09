using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Extensions;

namespace Ku.Core.CMS.Web.Security
{
    public class WebApiAuthAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class WebApiAuthAuthorizationHandler : DefaultAttributeAuthorizationHandler<WebApiAuthAuthorizationRequirement, MemberAuthAttribute>
    {
        private ICacheService _cacheService;

        public WebApiAuthAuthorizationHandler(ICacheService cacheService)
        {
            this._cacheService = cacheService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WebApiAuthAuthorizationRequirement requirement, List<MemberAuthAttribute> attributes, List<MemberAuthAttribute> controllAttributes)
        {
            // 检查缓存会员数据是否存在
            var member = _cacheService.Get<LoginMember>(string.Format(CacheKeyDefinition.ApiMemberToken, context.User.GetUserIdOrZero(), context.User.GetVersion()));
            if (member == null)
            {
                context.Fail();
                return;
            }

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
