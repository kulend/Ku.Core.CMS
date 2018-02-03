using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum.WeChat
{
    /// <summary>
    /// 微信二维码时效类型
    /// </summary>
    public enum EmWxPeriodType : short
    {
        [Display(Name = "临时")]
        Temp = 0,

        [Display(Name = "永久")]
        Forever = 1
    }
}
