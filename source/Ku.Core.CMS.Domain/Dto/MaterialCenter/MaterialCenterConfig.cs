using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Dto.MaterialCenter
{
    public class MaterialCenterConfig
    {
        [Required, StringLength(256)]
        [Display(Name = "图片保存路径")]
        public string PictureSavePath { set; get; }

        [Required, StringLength(256)]
        [Display(Name = "图片访问地址")]
        public string PictureUrl { set; get; }
    }
}
