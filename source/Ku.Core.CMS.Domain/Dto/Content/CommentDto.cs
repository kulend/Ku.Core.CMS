//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：CommentDto.cs
// 功能描述：评论 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 22:34
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentDto : BaseProtectedDto
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public long ArticleId { set; get; }

        /// <summary>
        /// 文章
        /// </summary>
        public virtual ArticleDto Article { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { set; get; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserCenter.UserDto User { set; get; }

        /// <summary>
        /// 是否回复
        /// </summary>
        public bool IsReply { set; get; }

        /// <summary>
        /// 父评论
        /// </summary>
        public long? ParentId { get; set; }

        [Required, StringLength(2000)]
        public string Content { set; get; }

        /// <summary>
        /// 点赞数
        /// </summary>
        [Display(Name = "点赞数")]
        public int Praises { set; get; } = 0;
    }
}
