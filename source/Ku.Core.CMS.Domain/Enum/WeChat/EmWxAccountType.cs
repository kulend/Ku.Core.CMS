using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.WeChat
{
    /// <summary>
    /// 公众号类型
    /// </summary>
    public enum EmWxAccountType : short
    {
        [Display(Name = "服务号")]
        Service = 1,

        [Display(Name = "订阅号")]
        Subscribe = 2,

        [Display(Name = "移动应用")]
        App = 3,

        [Display(Name = "小程序")]
        MiniApp = 4,

    }
}
