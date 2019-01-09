//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Article.cs
// 功能描述：文章 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Dnc.Extensions.Dapper.Attributes;
using Ku.Core.CMS.Domain.Enum.Content;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("content_article")]
    public class Article : BaseProtectedEntity
    {
        /// <summary>
        /// 栏目Id
        /// </summary>
        public long? ColumnId { set; get; }

        /// <summary>
        /// 栏目
        /// </summary>
        public virtual Column Column { set; get; }

        /// <summary>
        /// 专辑Id
        /// </summary>
        public long? AlbumId { set; get; }

        /// <summary>
        /// 专辑
        /// </summary>
        public virtual Album Album { set; get; }

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

    /// <summary>
    /// 文章 检索条件
    /// </summary>
    public class ArticleSearch : BaseProtectedSearch<Article>
    {
        public long? ColumnId { set; get; }

        public bool? IsPublished { set; get; }

        [Condition(Name = "ColumnId", Operation = ConditionOperation.In, WhenNull = WhenNull.Ignore)]
        public long[] ColumnIds { set; get; }

        [Condition(Name = "Tags", Operation = ConditionOperation.Like, LeftChar = "%|", RightChar = "|%",  WhenNull = WhenNull.Ignore)]
        public string HasTag { set; get; }

        [Condition(WhenNull = WhenNull.Ignore, Operation = ConditionOperation.Custom, CustomSql = "(Title LIKE @value OR SubTitle LIKE @value OR Keyword LIKE @value)")]
        public string Keyword { set; get; }

        /// <summary>
        /// 详情模式
        /// </summary>
        public EmArticleContentType? ContentType { set; get; }

        public long? AlbumId { set; get; }

        [Condition(WhenNull = WhenNull.Ignore, Operation = ConditionOperation.Custom, CustomSql = "((@value = 1 and AlbumId IS NOT NULL) or (@value = 0 and AlbumId IS NULL))")]
        public bool? InAlbum { set; get; }
    }
}
