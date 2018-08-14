//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IRoleService.cs
// 功能描述：角色 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 11:27
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IRoleService : IBaseService<Role, RoleDto, RoleSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(RoleDto dto);

        /// <summary>
        /// 保存权限
        /// </summary>
        Task SaveRoleAuthAsync(long RoleId, long FunctionId, bool hasAuth);
    }
}
