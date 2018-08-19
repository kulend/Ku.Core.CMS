using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.MaterialCenter
{
    /// <summary>
    /// 用户素材分组类型
    /// </summary>
    public enum EmUserMaterialGroupType : short
    {
        [Display(Name = "图片")]
        Picture = 1,

        [Display(Name = "视频")]
        Video = 2,
    }
}
