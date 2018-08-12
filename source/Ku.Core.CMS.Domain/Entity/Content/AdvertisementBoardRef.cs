//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementBoardRef.cs
// 功能描述：广告牌广告关联 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 22:22
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 广告牌广告关联
    /// </summary>
    [Table("content_advertisement_board_ref")]
    public class AdvertisementBoardRef
    {
        public long AdvertisementId { set; get; }
        public virtual Advertisement Advertisement { set; get; }

        public long AdvertisementBoardId { set; get; }
        public virtual AdvertisementBoard AdvertisementBoard { set; get; }
    }
}
