//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Video.cs
// 功能描述：视频素材 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 23:14
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.MaterialCenter
{
    /// <summary>
    /// 视频素材
    /// </summary>
    [Table("materialcenter_video")]
    public class Video : BaseMaterial
    {
        /// <summary>
        /// 预览图
        /// </summary>
        [StringLength(256)]
        public string ThumbPath { set; get; }

        /// <summary>
        /// 时长
        /// </summary>
        [StringLength(10)]
        [Display(Name = "时长")]
        public string Duration { set; get; }
    }

    /// <summary>
    /// 视频素材 检索条件
    /// </summary>
    public class VideoSearch : BaseProtectedSearch<Video>
    {

    }
}
