//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Order.cs
// 功能描述：订单 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-29 15:51
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Mall;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 订单
    /// </summary>
    [Table("mall_order")]
    public class Order : BaseProtectedEntity
    {

        public long? UserId { set; get; }

        public virtual UserCenter.User User { set; get; }

        #region Amount

        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ProductAmount { set; get; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { set; get; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountAmount { set; get; }

        /// <summary>
        /// 运费金额
        /// </summary>
        public decimal FreightAmount { set; get; }

        #endregion

        #region 配送信息

        /// <summary>
        /// 收货人
        /// </summary>
        [StringLength(20)]
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        [StringLength(20)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 配送地址
        /// </summary>
        public string DeliveryAddress { set; get; }

        /// <summary>
        /// 配送时间
        /// </summary>
        public DateTime? DeliveryTime { set; get; }

        #endregion

        #region 支付信息

        /// <summary>
        /// 是否已支付
        /// </summary>
        public bool IsPaid { set; get; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { set; get; }

        #endregion

        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime? ReceivedTime { set; get; }

        #region 退款

        /// <summary>
        /// 退款状态
        /// </summary>
        public EmOrderRefundStatus RefundStatus { set; get; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { set; get; }

        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime? RefundTime { set; get; }

        #endregion
    }

    /// <summary>
    /// 订单 检索条件
    /// </summary>
    public class OrderSearch : BaseProtectedSearch<Order>
    {

    }
}
