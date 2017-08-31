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
        [Display(Name = "新密码")]
        [DataType(DataType.Password)]
        public string NewPassword { set; get; }

        [Required, MaxLength(20)]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
    }
}
