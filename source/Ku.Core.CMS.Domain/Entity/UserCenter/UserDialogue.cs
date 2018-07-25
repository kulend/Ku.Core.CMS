//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDialogue.cs
// 功能描述：用户对话 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:23
//
//----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 用户对话
    /// </summary>
    [Table("usercenter_user_dialogue")]
    public class UserDialogue : BaseProtectedEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 最新消息摘要
        /// </summary>
        [StringLength(256)]
        public string LastMessageSummary { set; get; }

        /// <summary>
        /// 最新消息事件
        /// </summary>
        public DateTime LastMessageTime { set; get; }

        /// <summary>
        /// 是否禁言
        /// </summary>
        public bool IsForbidden { set; get; } = false;

        /// <summary>
        /// 是否已解决
        /// </summary>
        public bool IsSolved { set; get; } = false;

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? SolveTime { set; get; }

        /// <summary>
        /// 解决时间
        /// </summary>
        public long? SolveUserId { set; get; }
    }

    /// <summary>
    /// 用户对话 检索条件
    /// </summary>
    public class UserDialogueSearch : BaseProtectedSearch<UserDialogue>
    {

    }
}
