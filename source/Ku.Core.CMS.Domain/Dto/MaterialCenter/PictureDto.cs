//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PictureDto.cs
// 功能描述：图片素材 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-28 14:27
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.MaterialCenter
{
    /// <summary>
    /// 图片素材
    /// </summary>
    public class PictureDto : BaseMaterialDto
    {
        [StringLength(256)]
        [Display(Name = "缩略图路径")]
        public string ThumbPath { set; get; }

        [Display(Name = "缩略图地址")]
        public string ThumbUrl { set; get; }

        /// <summary>
        /// 是否已压缩
        /// </summary>
        [Display(Name = "是否已压缩", Prompt = "已压缩|未压缩")]
        public bool IsCompressed { set; get; }
    }
}
