//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserService.cs
// 功能描述：用户 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface IUserService : IBaseService<User, UserDto, UserSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserDto dto, long[] UserRoleIds);

        #region 其他接口

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        Task<List<RoleDto>> GetUserRolesAsync(long userId);

        /// <summary>
        /// 登陆
        /// </summary>
        Task<UserDto> LoginAsync(string account, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        Task ChangePassword(long userId, string currentPwd, string newPwd);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <returns></returns>
        Task<bool> PasswordCheckAsync(long id, string password);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        Task SaveProfileAsync(UserDto dto);

        #endregion
    }
}
