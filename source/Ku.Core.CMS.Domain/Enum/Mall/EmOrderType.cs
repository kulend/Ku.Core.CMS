using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmOrderType : short
    {
        [Display(Name = "普通订单")]
        Normal = 1,
    }
}
