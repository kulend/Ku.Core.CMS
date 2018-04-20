using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vino.Core.CMS.Web.Backend.Models
{
    public class PwdData
    {
        [Required, MaxLength(20)]
        [Display(Name = "当前密码")]
        [DataType(DataType.Password)]
        public string CurrentPassword { set; get; }

        [Required, MaxLength(20)]
        [Display(Name = "新密码", Description = "长度在6~20之间，只能包含字母、数字和下划线")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9]\w{5,19}$", ErrorMessage = "输入的密码不符合规则")]
        public string NewPassword { set; get; }

        [Required, MaxLength(20)]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [RegularExpression(@"custom|$('input[name=NewPassword]').val() === value", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { set; get; }
    }
}
