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
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserService : IBaseService<User, UserDto, UserSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserDto dto);
    }
}
