using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.Content
{
    public enum EmArticleUserEvent : short
    {
        [Display(Name = "点赞")]
        Praise = 1,

        [Display(Name = "收藏")]
        Collect = 2,
    }
}
