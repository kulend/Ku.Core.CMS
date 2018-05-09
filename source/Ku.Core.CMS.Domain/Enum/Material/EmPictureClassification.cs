using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Material
{
    public enum EmPictureClassification : short
    {
        [Display(Name = "默认")]
        General = 0,

        [Display(Name = "人物")]
        RichText = 1,

        [Display(Name = "动物")]
        Picture = 2,
    }
}
