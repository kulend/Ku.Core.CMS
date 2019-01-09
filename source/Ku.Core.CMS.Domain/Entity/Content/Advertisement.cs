//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Advertisement.cs
// 功能描述：广告 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 21:27
//
//----------------------------------------------------------------

using Dnc.Extensions.Dapper.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 广告
    /// </summary>
    [Table("content_advertisement")]
    public class Advertisement : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(128)]
        public string Name { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string Title { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "图片")]
        public string ImageData { set; get; }

        /// <summary>
        /// Flash地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "Flash地址")]
        public string FlashUrl { set; get; }

        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(512)]
        public string Link { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        [MaxLength(64)]
        [Display(Name = "来源")]
        public string Provenance { set; get; }

        /// 是否发布
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "是否发布", Prompt = "是|否")]
        public bool IsPublished { set; get; } = true;

        /// <summary>
        /// 点击数
        /// </summary>
        [Display(Name = "点击数")]
        public int Clicks { set; get; } = 0;

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }
    }

    /// <summary>
    /// 广告 检索条件
    /// </summary>
    public class AdvertisementSearch : BaseProtectedSearch<Advertisement>
    {
        [Condition(WhenNull = WhenNull.Ignore, Operation = ConditionOperation.Custom, CustomSql = "EXISTS (SELECT * FROM content_advertisement_board_ref ref WHERE ref.AdvertisementId=m.Id AND ref.AdvertisementBoardId=@value)")]
        public long? AdvertisementBoardId { set; get; }

        public bool? IsPublished { set; get; }
    }
}
