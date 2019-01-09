//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAlbumService.cs
// 功能描述：专辑 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-12-27 07:48
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IAlbumService : IBaseService<Album, AlbumDto, AlbumSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AlbumDto dto);

        /// <summary>
        /// 增加点击数
        /// </summary>
        Task<bool> IncreaseVisitsAsync(long id, int count = 1);
    }
}
