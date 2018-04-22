//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductSkuDto.cs
// 功能描述：商品SKU 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum;
using Vino.Core.CMS.Domain.Enum.Mall;
using Vino.Core.Infrastructure.Attributes;

namespace Vino.Core.CMS.Domain.Dto.Mall
{
    public class ProductSkuDto : BaseProtectedDto
    {
        [DataType("Hidden")]
        public long ProductId { set; get; }

        public ProductDto Product { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EmProductSkuStatus Status { set; get; } = EmProductSkuStatus.OnSale;

        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 封面
        /// </summary>
        [MaxLength(500)]
        [Display(Name = "封面")]
        public string CoverImage { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        [DisplayFormat(DataFormatString = "C")]
        public decimal Price { set; get; }

        /// <summary>
        /// 市场价
        /// </summary>
        [Display(Name = "市场价" )]
        [DisplayFormat(DataFormatString = "C")]
        public decimal MarketPrice { set; get; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "库存")]
        [DisplayFormat(DataFormatString = "#,0 件")]
        public int Stock { set; get; } = 0;

        /// <summary>
        /// 销售量
        /// </summary>
        [Display(Name = "销售量")]
        [DisplayFormat(DataFormatString = "#,0 件")]
        public int Sales { set; get; } = 0;

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }

        /// <summary>
        /// 重量
        /// </summary>
        [Display(Name = "重量")]
        [DisplayFormat(DataFormatString = "#,0 克")]
        public int Weight { set; get; }

        /// <summary>
        /// 积分赠送规则
        /// </summary>
        [Display(Name = "积分赠送规则")]
        public EmPointsGainRule PointsGainRule { set; get; }

        /// <summary>
        /// 积分赠送规则
        /// </summary>
        [Display(Name = "赠送积分")]
        [DisplayFormat(DataFormatString = "#,0 积分")]
        public int GainPoints { set; get; }

        public EmEntityModifyStatus ModifyStatus { set; get; } = EmEntityModifyStatus.UnChange;
    }
}
