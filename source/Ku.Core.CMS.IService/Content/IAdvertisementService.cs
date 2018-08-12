//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAdvertisementService.cs
// 功能描述：广告 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 21:27
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IAdvertisementService : IBaseService<Advertisement, AdvertisementDto, AdvertisementSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AdvertisementDto dto, long[] boards);

        /// <summary>
        /// 取得广告所在广告位列表
        /// </summary>
        Task<IEnumerable<AdvertisementBoardDto>> GetAdvertisementBoardsAsync(long id);
    }
}
