//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppFeedbackDto.cs
// 功能描述：应用反馈 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vino.Core.CMS.Domain.Dto.DataCenter
{
    /// <summary>
    /// 应用反馈
    /// </summary>
    public class AppFeedbackDto : BaseProtectedDto
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [DataType("Hidden")]
        public long AppId { set; get; }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual AppDto App { set; get; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        [Required, StringLength(2000)]
        [Display(Name = "反馈内容")]
        public string Content { set; get; }

        /// <summary>
        /// 反馈者ID
        /// </summary>
        public long? ProviderId { set; get; }

        /// <summary>
        /// 反馈者名称
        /// </summary>
        [StringLength(64)]
        [Display(Name = "反馈者")]
        public string ProviderName { set; get; }

        /// <summary>
        /// 是否已解决
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "是否已解决", Prompt = "已解决|未解决")]
        public bool Resolved { set; get; } = false;
    }
}
