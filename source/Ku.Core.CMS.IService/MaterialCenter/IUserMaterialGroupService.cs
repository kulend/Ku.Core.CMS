//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserMaterialGroupService.cs
// 功能描述：用户素材分组 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-18 11:31
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.MaterialCenter
{
    public partial interface IUserMaterialGroupService : IBaseService<UserMaterialGroup, UserMaterialGroupDto, UserMaterialGroupSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserMaterialGroupDto dto);
    }
}
