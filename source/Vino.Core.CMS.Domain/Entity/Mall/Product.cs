//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Product.cs
// 功能描述：商品 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.Mall;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    [Table("mall_product")]
    public class Product : BaseProtectedEntity
    {
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EmProductStatus Status { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(128)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(128)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "简介")]
        public string Intro { set; get; }

        /// <summary>
        /// 主图
        /// </summary>
        [MaxLength(3000)]
        [Display(Name = "主图")]
        public string ImageData { set; get; }

        /// <summary>
        /// 详情模式
        /// </summary>
        [Display(Name = "详情模式")]
        public EmProductContentType ContentType { set; get; }

        /// <summary>
        /// 详情内容
        /// </summary>
        [Display(Name = "详情内容")]
        public string Content { set; get; }

        /// <summary>
        /// 价格区间
        /// </summary>
        [MaxLength(32)]
        [Display(Name = "价格")]
        public string PriceRange { set; get; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "总库存")]
        public int Stock { set; get; } = 0;

        /// <summary>
        /// 销售量
        /// </summary>
        [Display(Name = "总销售量")]
        public int Sales { set; get; } = 0;

        /// <summary>
        /// 浏览量
        /// </summary>
        [Display(Name = "总浏览量")]
        public int Visits { set; get; } = 0;

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }

        /// <summary>
        /// 商品属性
        /// </summary>
        [MaxLength(2000)]
        [Display(Name = "商品属性")]
        public string Properties { set; get; }

        /// <summary>
        /// 是否快照
        /// </summary>
        [Display(Name = "是否快照", Prompt = "是|否")]
        public bool IsSnapshot { set; get; } = false;

        /// <summary>
        /// 快照数
        /// </summary>
        [Display(Name = "快照数")]
        public int SnapshotCount { set; get; }

        #region 快照数据

        [Display(Name = "生效时间")]
        public DateTime? EffectiveTime { set; get; }

        [Display(Name = "失效时间")]
        public DateTime? ExpireTime { set; get; }

        /// <summary>
        /// 原ID
        /// </summary>
        public long? OriginId { set; get; }

        #endregion
    }

    public class ProductSearch : BaseSearch<Product>
    {
        public bool? IsSnapshot { set; get; }
    }
}
