using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmOrderStatus : short
    {
        [Display(Name = "已取消")]
        Cancel = 99,

        [Display(Name = "待支付")]
        WaitPay = 0,

        [Display(Name = "待配送")]
        WaitDelivery = 5,

        [Display(Name = "配送中")]
        Deliverying = 6,

        [Display(Name = "待收货")]
        WaitReceive = 10,

        [Display(Name = "待评价")]
        WaitEvaluate = 15,


    }
}
