using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public interface IBackendAuthService
    {
        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        Task<bool> CheckUserAuthAsync(long userId, string authCode);

        Task<List<string>> GetUserAuthCodesAsync(long userId, bool encrypt = false);
    }
}
