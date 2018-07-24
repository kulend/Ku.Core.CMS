using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.System
{
    public enum EmTimedTaskStatus : short
    {
        [Display(Name = "未启用")]
        Disable = 0,
        [Display(Name = "正常")]
        Enable = 1,
        [Display(Name = "运行中")]
        Running = 2,
        [Display(Name = "暂停中")]
        Pause = 3,
        [Display(Name = "已过期")]
        Expired = 4,
        [Display(Name = "异常")]
        Abnormal = 5,
    }
}
