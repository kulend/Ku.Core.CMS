using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmProductStatus : short
    {

        [Display(Name = "准备中")]
        Preparing = 0,

        [Display(Name = "销售中")]
        OnSale = 1,
    }
}
