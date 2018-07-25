//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDialogueDto.cs
// 功能描述：用户对话 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:23
//
//----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 用户对话
    /// </summary>
    public class UserDialogueDto : BaseProtectedDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserDto User { get; set; }

        /// <summary>
        /// 最新消息摘要
        /// </summary>
        [StringLength(256)]
        public string LastMessageSummary { set; get; }

        /// <summary>
        /// 最新消息时间
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
}
