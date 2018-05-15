//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMaterialGroupService.cs
// 功能描述：素材分组 业务逻辑接口类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Material;
using Ku.Core.CMS.Domain.Entity.Material;

namespace Ku.Core.CMS.IService.Material
{
    public partial interface IMaterialGroupService : IBaseService<MaterialGroup, MaterialGroupDto, MaterialGroupSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MaterialGroupDto dto);
    }
}
