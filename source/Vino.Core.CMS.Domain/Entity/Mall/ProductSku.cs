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
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    [Table("mall_product_sku")]
    public class ProductSku : BaseProtectedEntity
    {
        [DataType("Hidden")]
        public long ProductId { set; get; }

        public Product Product { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "名称")]
        public string Name { set; get; }

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
    }

    public class ProductSkuSearch : BaseSearch<ProductSku>
    {

    }
}
