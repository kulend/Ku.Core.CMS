//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Payment.cs
// 功能描述：支付方式 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-08 13:31
//
//----------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.Mall;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 支付方式
    /// </summary>
    [Table("mall_payment")]
    public class Payment : BaseProtectedEntity
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
        /// 是否移动支付
        /// </summary>
        [Display(Name = "是否移动支付", Prompt = "是|否")]
        public bool IsMobile { set; get; } = true;

        /// <summary>
        /// 支付类型
        /// </summary>
        [DefaultValue(EmPaymentMode.Alipay)]
        [Display(Name = "支付类型")]
        public EmPaymentMode PaymentMode { set; get; } = EmPaymentMode.Alipay;

        [Display(Name = "支付配置")]
        public string PaymentConfig { set; get; }

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

    /// <summary>
    /// 支付方式 检索条件
    /// </summary>
    public class PaymentSearch : BaseProtectedSearch<Payment>
    {
        public bool? IsSnapshot { set; get; }
    }
}
