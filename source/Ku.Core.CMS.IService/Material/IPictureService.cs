//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IPictureService.cs
// 功能描述：图片素材 业务逻辑接口
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
    public partial interface IPictureService : IBaseService<Picture, PictureDto, PictureSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(PictureDto dto);

        Task AddAsync(PictureDto dto);
    }
}
