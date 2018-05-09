using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmProductSkuStatus : short
    {

        [Display(Name = "停售")]
        OffSale = 0,

        [Display(Name = "在售")]
        OnSale = 1,
    }
}
