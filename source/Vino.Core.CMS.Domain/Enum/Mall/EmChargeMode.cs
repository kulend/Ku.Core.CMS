using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum.Mall
{
    public enum EmChargeMode : short
    {

        [Display(Name = "按件数")]
        Quantity = 1,

        [Display(Name = "按重量")]
        Weight = 2,
    }
}
