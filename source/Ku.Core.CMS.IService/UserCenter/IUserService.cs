//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserService.cs
// 功能描述：用户 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 10:45
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserService : IBaseService<User, UserDto, UserSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserDto dto, long[] userRoles);

        Task<List<RoleDto>> GetUserRolesAsync(long userId);

        Task<UserDto> LoginAsync(string account, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        Task ChangePasswordAsync(long userId, string currentPwd, string newPwd);

        /// <summary>
        /// 用户资料保存
        /// </summary>
        Task SaveProfileAsync(UserDto dto);

        /// <summary>
        /// 密码验证
        /// </summary>
        Task<bool> PasswordCheckAsync(long id, string password);
    }
}
