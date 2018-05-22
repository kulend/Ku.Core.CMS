using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Communication
{
    public enum EmSmsQueueStatus : short
    {
        [Display(Name = "待发送")]
        WaitSend = 0,

        [Display(Name = "已发送")]
        Sent = 1,

        [Display(Name = "已过期")]
        Expired = 2,

        [Display(Name = "发送出错")]
        Error = 3,
    }
}
