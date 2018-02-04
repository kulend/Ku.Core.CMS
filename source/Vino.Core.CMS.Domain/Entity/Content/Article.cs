//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Article.cs
// 功能描述：文章 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Domain.Enum.Content;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Content
{
    [Table("content_article")]
    public class Article : BaseProtectedEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(256)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 副标题
        /// </summary>
        [Required, MaxLength(256)]
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
    }

    public class ArticleSearch : BaseSearch<Article>
    {

    }
}
