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

        [Required, StringLength(256)]
        [Display(Name = "文件保存路径")]
        public string FileSavePath { set; get; }

        [Required, StringLength(256)]
        [Display(Name = "文件访问地址")]
        public string FileSiteUrl { set; get; }
    }
}
