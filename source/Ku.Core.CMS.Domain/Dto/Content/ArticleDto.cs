//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ArticleDto.cs
// 功能描述：文章 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Ku.Core.CMS.Domain.Base;
using Ku.Core.CMS.Domain.Enum.Content;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleDto : BaseProtectedDto
    {
        /// <summary>
        /// 栏目Id
        /// </summary>
        [DataType("hidden")]
        public long? ColumnId { set; get; }

        /// <summary>
        /// 栏目
        /// </summary>
        public virtual ColumnDto Column { set; get; }

        /// <summary>
        /// 专辑Id
        /// </summary>
        [DataType("hidden")]
        public long? AlbumId { set; get; }

        /// <summary>
        /// 专辑
        /// </summary>
        public virtual AlbumDto Album { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(256)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 副标题
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "副标题")]
        public string SubTitle { set; get; }

        /// <summary>
        /// 作者
        /// </summary>
        [MaxLength(32)]
        [Display(Name = "作者")]
        public string Author { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        [MaxLength(64)]
        [Display(Name = "来源")]
        public string Provenance { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(3000)]
        [Display(Name = "简介")]
        [DataType(DataType.MultilineText)]
        public string Intro { set; get; }

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }

        /// <summary>
        /// 关键字
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "关键字")]
        public string Keyword { set; get; }

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
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? PublishedTime { set; get; }

        /// <summary>
        /// 详情模式
        /// </summary>
        [Display(Name = "详情模式")]
        public EmArticleContentType ContentType { set; get; }

        /// <summary>
        /// 详情内容
        /// </summary>
        [Display(Name = "详情内容")]
        public string Content { set; get; }

        /// <summary>
        /// 浏览量
        /// </summary>
        [Display(Name = "浏览量")]
        public int Visits { set; get; } = 0;

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
        /// 点赞数
        /// </summary>
        [Display(Name = "点赞数")]
        public int Praises { set; get; } = 0;

        /// <summary>
        /// 收藏数
        /// </summary>
        [Display(Name = "收藏数")]
        public int Collects { set; get; } = 0;

        /// <summary>
        /// 标签
        /// </summary>
        [MaxLength(128)]
        [Display(Name = "标签")]
        public string Tags { set; get; }

        /// <summary>
        /// 评论数
        /// </summary>
        [Display(Name = "评论数")]
        public int Comments { set; get; } = 0;

        /// <summary>
        /// 时长
        /// </summary>
        [Display(Name = "时长")]
        [MaxLength(10)]
        public string Duration { set; get; }
    }
}
