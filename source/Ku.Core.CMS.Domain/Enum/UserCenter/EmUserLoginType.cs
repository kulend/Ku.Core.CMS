using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.UserCenter
{
    /// <summary>
    /// 用户登录类型
    /// </summary>
    public enum EmUserLoginType : short
    {
        [Display(Name = "QQ")]
        QQ = 1,

        [Display(Name = "微信")]
        Weixin = 2,
    }
}
