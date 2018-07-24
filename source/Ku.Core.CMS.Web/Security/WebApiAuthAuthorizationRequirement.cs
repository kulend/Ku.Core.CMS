using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Security
{
    public class WebApiAuthAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class WebApiAuthAuthorizationHandler : DefaultAttributeAuthorizationHandler<WebApiAuthAuthorizationRequirement, UserAuthAttribute>
    {
        private ICacheProvider _cacheService;

        public WebApiAuthAuthorizationHandler(ICacheProvider cacheService)
        {
            this._cacheService = cacheService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WebApiAuthAuthorizationRequirement requirement, List<UserAuthAttribute> attributes, List<UserAuthAttribute> superAttributes)
        {
            // 检查缓存会员数据是否存在
            var user = await _cacheService.GetAsync<LoginMember>(string.Format(CacheKeyDefinition.ApiUserToken, context.User.GetUserIdOrZero(), context.User.GetVersion()));
            if (user == null)
            {
                context.Fail();
                return;
            }

            var attr1 = superAttributes.FirstOrDefault();
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

        private bool CheckPermissionAsync(AuthorizationHandlerContext context, UserAuthAttribute attribute)
        {
            if (attribute == null)
            {
                return true;
            }
            if (context.User == null)
            {
                return false;
            }
            var role = context.User.GetUserRole();
            if (!role.HasValue) return false;
            return (attribute.Role & role.Value) != 0;
        }
    }
}
