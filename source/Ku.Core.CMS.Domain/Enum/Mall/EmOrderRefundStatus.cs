using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmOrderRefundStatus : short
    {

        [Display(Name = "未退款")]
        None = 0,

        [Display(Name = "退款中")]
        Process = 1,

        [Display(Name = "已退款")]
        Complete = 2,
    }
}
