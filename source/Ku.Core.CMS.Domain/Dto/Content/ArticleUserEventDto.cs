//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ArticleUserEventDto.cs
// 功能描述：文章用户事件 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-09-14 22:48
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Content;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 文章用户事件
    /// </summary>
    public class ArticleUserEventDto : BaseProtectedDto
    {
        public long ArticleId { set; get; }

        public virtual ArticleDto Article { set; get; }

        public long UserId { set; get; }

        public virtual UserCenter.UserDto User { set; get; }

        public EmArticleUserEvent Event { set; get; }

        public bool IsCancel { set; get; } = true;

        public DateTime EventTime { set; get; }

        public DateTime? CancelTime { set; get; }
    }
}
