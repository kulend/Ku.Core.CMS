//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ArticleUserEvent.cs
// 功能描述：文章用户事件 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-09-14 22:48
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Content;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 文章用户事件
    /// </summary>
    [Table("content_article_user_event")]
    public class ArticleUserEvent : BaseProtectedEntity
    {
        public long ArticleId { set; get; }

        public virtual Article Article { set; get; }

        public long UserId { set; get; }

        public virtual UserCenter.User User { set; get; }

        public EmArticleUserEvent Event { set; get; }

        public bool IsCancel { set; get; } = true;

        public DateTime EventTime { set; get; }

        public DateTime? CancelTime { set; get; }
    }

    /// <summary>
    /// 文章用户事件 检索条件
    /// </summary>
    public class ArticleUserEventSearch : BaseProtectedSearch<ArticleUserEvent>
    {
        public EmArticleUserEvent? Event { set; get; }

        public long? UserId { set; get; }

        public bool? IsCancel { set; get; }
    }
}
