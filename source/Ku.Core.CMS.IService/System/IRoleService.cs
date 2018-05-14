//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IRoleService.cs
// 功能描述：角色 业务逻辑接口
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
    public partial interface IRoleService : IBaseService<Role, RoleDto, RoleSearch>
    {

        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(RoleDto dto);

        #region 其他接口

        /// <summary>
        /// 取得功能列表（附带角色是否有权限）
        /// </summary>
        Task<List<FunctionDto>> GetFunctionsWithRoleAuthAsync(long roleId, long? parentFunctionId);

        Task SaveRoleAuthAsync(long RoleId, long FunctionId, bool hasAuth);

        #endregion
    }
}
