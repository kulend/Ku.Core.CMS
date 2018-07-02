//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：OrderProduct.cs
// 功能描述：订单商品 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-30 09:11
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 订单商品
    /// </summary>
    [Table("mall_order_product")]
    public class OrderProduct : BaseProtectedEntity
    {
        public long OrderId { set; get; }

        public virtual Order Order { set; get; }

        public long ProductId { set; get; }

        public virtual Product Product { set; get; }

        public long ProductSkuId { set; get; }

        public virtual ProductSku ProductSku { set; get; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal ProductPrice { set; get; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Qty { set; get; }

        /// <summary>
        /// 商品总额
        /// </summary>
        public decimal Amount { set; get; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [StringLength(256)]
        public string Remark { set; get; }

    }

    /// <summary>
    /// 订单商品 检索条件
    /// </summary>
    public class OrderProductSearch : BaseProtectedSearch<OrderProduct>
    {

    }
}
