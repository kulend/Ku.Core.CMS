//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDialogueMessageDto.cs
// 功能描述：用户对话消息 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:24
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 用户对话消息
    /// </summary>
    public class UserDialogueMessageDto : BaseProtectedDto
    {
        /// <summary>
        /// 对话ID
        /// </summary>
        public long DialogueId { get; set; }

        /// <summary>
        /// 对话
        /// </summary>
        public virtual UserDialogueDto Dialogue { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserDto User { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        /// <summary>
        /// 最新消息摘要
        /// </summary>
        [Required, StringLength(1024)]
        public string Message { set; get; }
    }
}
