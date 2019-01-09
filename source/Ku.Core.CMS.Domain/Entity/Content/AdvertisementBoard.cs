//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementBoard.cs
// 功能描述：广告牌 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 22:15
//
//----------------------------------------------------------------

using Dnc.Extensions.Dapper.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 广告牌
    /// </summary>
    [Table("content_advertisement_board")]
    public class AdvertisementBoard : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(64)]
        public string Name { set; get; }

        /// <summary>
        /// 标记
        /// </summary>
        [Required, StringLength(32)]
        public string Tag { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;
    }

    /// <summary>
    /// 广告牌 检索条件
    /// </summary>
    public class AdvertisementBoardSearch : BaseProtectedSearch<AdvertisementBoard>
    {
        [Condition(Operation = ConditionOperation.Custom, CustomSql = "EXISTS (SELECT * FROM content_advertisement_board_ref ref WHERE ref.AdvertisementBoardId=m.Id AND ref.AdvertisementId=@value)")]
        public long? AdvertisementId { set; get; }

        public bool? IsEnable { set; get; }

        public string Tag { set; get; }
    }
}
