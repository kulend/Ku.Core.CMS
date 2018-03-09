using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum
{
    /// <summary>
    /// 实例编辑状态
    /// </summary>
    public enum EmEntityModifyStatus
    {
        [Display(Name = "未变动")]
        UnChange = 0,

        [Display(Name = "新增")]
        Insert = 1,

        [Display(Name = "更新")]
        Update = 2,

        [Display(Name = "删除")]
        Delete = 3
    }
}
