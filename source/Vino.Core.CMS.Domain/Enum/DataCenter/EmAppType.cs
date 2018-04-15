using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum.DataCenter
{
    public enum EmAppType : short
    {
        [Display(Name = "安卓")]
        Android = 0,

        [Display(Name = "IOS")]
        Ios = 1,
    }
}
