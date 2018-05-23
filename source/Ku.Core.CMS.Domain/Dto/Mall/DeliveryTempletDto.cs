//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：DeliveryTempletDto.cs
// 功能描述：配送模板 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
//
//----------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ku.Core.CMS.Domain.Enum.Mall;

namespace Ku.Core.CMS.Domain.Dto.Mall
{
    /// <summary>
    /// 配送模板
    /// </summary>
    public class DeliveryTempletDto : BaseProtectedDto
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
        [DataType(DataType.MultilineText)]
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

        public QuantityConfigModel QuantityChargeConfigObj
        {
            get {
                if (ChargeMode == EmChargeMode.Quantity)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<QuantityConfigModel>(ChargeConfig);
                    }
                    catch (Exception)
                    {

                        return new QuantityConfigModel();
                    }
                }
                else
                {
                    return new QuantityConfigModel();
                }
            }
        }

        public WeightConfigModel WeightChargeConfigObj
        {
            get
            {
                if (ChargeMode == EmChargeMode.Quantity)
                {
                    return new WeightConfigModel();
                }
                else
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<WeightConfigModel>(ChargeConfig);
                    }
                    catch (Exception)
                    {

                        return new WeightConfigModel();
                    }
                }
            }
        }

        public class QuantityConfigModel
        {
            public decimal DefaultFee { set; get; }

            public decimal PreQuantityFee { set; get; }

            public List<QuantityConfigAreaModel> AreaConfig { set; get; }
        }

        public class WeightConfigModel
        {
            public int FirstWeight { set; get; }

            public decimal FirstFee { set; get; }

            public int ExtendWeight { set; get; }

            public decimal ExtendFee { set; get; }

            public List<WeightConfigAreaModel> AreaConfig { set; get; }
        }

        public class QuantityConfigAreaModel
        {
            public List<AreaModel> Areas { set; get; }

            public bool Deliverable { set; get; }

            public decimal DefaultFee { set; get; }

            public decimal PreQuantityFee { set; get; }
        }

        public class WeightConfigAreaModel
        {
            public List<AreaModel> Areas { set; get; }

            public bool Deliverable { set; get; }

            public int FirstWeight { set; get; }

            public decimal FirstFee { set; get; }

            public int ExtendWeight { set; get; }

            public decimal ExtendFee { set; get; }
        }

        public class AreaModel
        {
            public string Code { set; get; }
            public string Name { set; get; }
        }

    }
}
