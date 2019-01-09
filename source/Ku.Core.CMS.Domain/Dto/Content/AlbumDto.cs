//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AlbumDto.cs
// 功能描述：专辑 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-12-27 07:48
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 专辑
    /// </summary>
    public class AlbumDto : BaseProtectedDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required, StringLength(128)]
        public string Title { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(2000)]
        [Display(Name = "简介")]
        public string Intro { set; get; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "是否发布", Prompt = "是|否")]
        public bool IsPublished { set; get; } = false;

        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        public DateTime? PublishedTime { set; get; }

        /// <summary>
        /// 封面
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "封面")]
        public string CoverData { set; get; }

        /// <summary>
        /// 封面
        /// </summary>
        public virtual JsonUploadImage Cover
        {
            get
            {
                return JsonUploadImage.Parse(CoverData).FirstOrDefault();
            }
        }

        /// <summary>
        /// 标签
        /// </summary>
        [StringLength(128)]
        public string Tags { set; get; }

        /// <summary>
        /// 视频数
        /// </summary>
        [Display(Name = "视频数")]
        public int Videos { set; get; } = 0;

        /// <summary>
        /// 浏览量
        /// </summary>
        [Display(Name = "浏览量")]
        public int Visits { set; get; } = 0;

        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderIndex { set; get; } = 0;
    }
}
