//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IVideoService.cs
// 功能描述：视频素材 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 23:14
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.MaterialCenter
{
    public partial interface IVideoService : IBaseService<Video, VideoDto, VideoSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(VideoDto dto);

        Task AddAsync(VideoDto dto);
    }
}
