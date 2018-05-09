//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：PaymentDto.cs
// 功能描述：支付方式 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-08 13:31
//
//----------------------------------------------------------------

using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.Mall;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Dto.Mall
{
    public class PaymentDto : BaseProtectedDto
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
        public IDictionary<string, string> PaymentConfig { set; get; }

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
}
