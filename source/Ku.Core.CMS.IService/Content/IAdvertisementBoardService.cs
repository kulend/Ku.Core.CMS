//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAdvertisementBoardService.cs
// 功能描述：广告牌 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 22:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IAdvertisementBoardService : IBaseService<AdvertisementBoard, AdvertisementBoardDto, AdvertisementBoardSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AdvertisementBoardDto dto);
    }
}
