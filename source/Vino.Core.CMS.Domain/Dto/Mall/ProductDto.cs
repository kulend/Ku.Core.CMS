//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductDto.cs
// 功能描述：商品 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.Mall;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Dto.Mall
{
    public class ProductDto : BaseProtectedDto
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
        /// 商品类目ID
        /// </summary>
        public long? CategoryId { set; get; }

        /// <summary>
        /// 商品类目
        /// </summary>
        public virtual ProductCategoryDto Category { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "简介")]
        [DataType(DataType.MultilineText)]
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
        [Display(Name = "商品属性")]
        public List<ProductPropertyItem> Properties { set; get; }

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
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? EffectiveTime { set; get; }

        [Display(Name = "失效时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? ExpireTime { set; get; }

        /// <summary>
        /// 原ID
        /// </summary>
        public long? OriginId { set; get; }

        #endregion
    }

    public class ProductPropertyItem
    {
        public string Name { set; get; }

        public string Value { set; get; }
    }
}
