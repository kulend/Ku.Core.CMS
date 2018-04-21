using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum.Membership
{
    /// <summary>
    /// 积分变动业务类型
    /// </summary>
    public enum EmMemberPointBusType : short
    {
        [Display(Name = "管理员调整")]
        AdminAdjust = 1,

        [Display(Name = "其他")]
        Other = 99,
    }
}
