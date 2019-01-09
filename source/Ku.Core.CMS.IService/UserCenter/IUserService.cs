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
using Ku.Core.CMS.Domain.Enum.UserCenter;
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

        Task<UserDto> MobileLoginAsync(string mobile, string password);

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

        /// <summary>
        /// 登陆(第三方登录)
        /// </summary>
        Task<UserDto> OAuthLoginAsync(EmUserLoginType type, string openId);

        /// <summary>
        /// 绑定(第三方登录)
        /// </summary>
        Task OAuthBindAsync(long userId, EmUserLoginType type, string openId);

        /// <summary>
        /// 注册
        /// </summary>
        Task<UserDto> RegisterAsync(string mobile, string password);

        /// <summary>
        /// 手机绑定
        /// </summary>
        Task MobileBindAsync(long userId, string mobile);

        /// <summary>
        /// 重置密码
        /// </summary>
        Task ResetPasswordAsync(string mobile, string password);

        /// <summary>
        /// 用户头像保存
        /// </summary>
        Task SaveHeadImageAsync(long userId, string HeadImagePath);
    }
}
