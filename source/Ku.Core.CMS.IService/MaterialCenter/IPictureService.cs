//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IPictureService.cs
// 功能描述：图片素材 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-28 14:27
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.MaterialCenter
{
    public partial interface IPictureService : IBaseService<Picture, PictureDto, PictureSearch>
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        Task AddAsync(PictureDto dto);

        /// <summary>
        /// 更新
        /// </summary>
        Task UpdateAsync(PictureDto dto);
    }
}
