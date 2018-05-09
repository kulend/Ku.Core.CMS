using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Mall
{
    public enum EmProductContentType : short
    {
        [Display(Name = "纯文本")]
        Text = 0,

        [Display(Name = "富文本")]
        RichText = 1,

        [Display(Name = "多图")]
        Picture = 2,
    }
}
