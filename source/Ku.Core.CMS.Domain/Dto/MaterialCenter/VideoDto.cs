//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：VideoDto.cs
// 功能描述：视频素材 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 23:14
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.MaterialCenter
{
    /// <summary>
    /// 视频素材
    /// </summary>
    public class VideoDto : BaseMaterialDto
    {
        /// <summary>
        /// 预览图
        /// </summary>
        [StringLength(256)]
        [Display(Name = "预览图")]
        public string ThumbPath { set; get; }

        /// <summary>
        /// 时长
        /// </summary>
        [StringLength(10)]
        [Display(Name = "时长")]
        public string Duration { set; get; }

        /// <summary>
        /// 预览图
        /// </summary>
        [StringLength(256)]
        [Display(Name = "预览图Url")]
        public string ThumbUrl { set; get; }
    }
}
