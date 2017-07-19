using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Vino.Core.CMS.Web.Security
{
    public class ValidJtiRequirement : IAuthorizationRequirement
    {

    }

    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        //private IAdminTokenBlacklistRepository _blacklistRepository;

        //public ValidJtiHandler(IAdminTokenBlacklistRepository blacklistRepository)
        //{
        //    _blacklistRepository = blacklistRepository;
        //}

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {
            //// 检查 Jti 是否存在
            //var jti = context.User.GetTokenIdOrNull();
            //if (string.IsNullOrEmpty(jti))
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}
            //// 检查 jti 是否在黑名单
            //var tokenExists = _blacklistRepository.GetById(jti);
            //if (tokenExists != null)
            //{
            //    context.Fail();
            //}
            //else
            //{
            //    context.Succeed(requirement); // 显式的声明验证成功
            //}

            //TODO
            context.Succeed(requirement); // 显式的声明验证成功
            return Task.CompletedTask;
        }
    }
}
