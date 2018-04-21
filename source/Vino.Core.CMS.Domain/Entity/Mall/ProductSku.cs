//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductSku.cs
// 功能描述：商品SKU 实体类
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

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    [Table("mall_product_sku")]
    public class ProductSku : BaseProtectedEntity
    {
        [DataType("Hidden")]
        public long ProductId { set; get; }

        public Product Product { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EmProductSkuStatus Status { set; get; }

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
        public string CoverImage{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        public decimal Price { set; get; }

        /// <summary>
        /// 市场价
        /// </summary>
        [Display(Name = "市场价")]
        public decimal MarketPrice { set; get; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "库存")]
        public int Stock { set; get; } = 0;

        /// <summary>
        /// 销售量
        /// </summary>
        [Display(Name = "销售量")]
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
        public int GainPoints { set; get; }

        [NotMapped]
        public EmEntityModifyStatus ModifyStatus { set; get; } = EmEntityModifyStatus.UnChange;
    }

    public class ProductSkuSearch : BaseProtectedSearch<ProductSku>
    {
        public long? ProductId { set; get; }
    }
}
