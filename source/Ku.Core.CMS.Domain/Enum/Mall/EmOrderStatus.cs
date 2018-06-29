using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmOrderStatus : short
    {

        [Display(Name = "按件数")]
        Quantity = 1,

        [Display(Name = "按重量")]
        Weight = 2,
    }
}
