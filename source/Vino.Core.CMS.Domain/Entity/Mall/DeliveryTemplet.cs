//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：DeliveryTemplet.cs
// 功能描述：配送模板 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
//
//----------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.Mall;
using Vino.Core.Infrastructure.Attributes;

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 运费模板
    /// </summary>
    [Table("mall_delivery_templet")]
    public class DeliveryTemplet : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "说明")]
        public string Description { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 计费方式
        /// </summary>
        [DefaultValue(EmChargeMode.Quantity)]
        [Display(Name = "计费方式")]
        public EmChargeMode ChargeMode { set; get; } = EmChargeMode.Quantity;

        [Display(Name = "计费配置")]
        public string ChargeConfig { set; get; }

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

    public class DeliveryTempletSearch : BaseProtectedSearch<DeliveryTemplet>
    {
        public bool? IsSnapshot { set; get; }
    }
}
