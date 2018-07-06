using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Content
{
    public enum EmMaskWordLevel : short
    {
        [Display(Name = "危险")]
        Danger = 1,

        [Display(Name = "警告")]
        Warn = 2,

        [Display(Name = "一般")]
        General = 3,
    }
}
