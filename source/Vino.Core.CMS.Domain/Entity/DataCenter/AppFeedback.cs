//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppFeedback.cs
// 功能描述：应用反馈 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vino.Core.CMS.Domain.Entity.DataCenter
{
    /// <summary>
    /// 应用反馈
    /// </summary>
    [Table("datacenter_app_feedback")]
    public class AppFeedback : BaseProtectedEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public long AppId { set; get; }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual App App { set; get; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        [Required, StringLength(2000)]
        public string Content { set; get; }

        /// <summary>
        /// 反馈者ID
        /// </summary>
        public long? ProviderId { set; get; }

        /// <summary>
        /// 反馈者名称
        /// </summary>
        [StringLength(64)]
        public string ProviderName { set; get; }

        /// <summary>
        /// 是否已解决
        /// </summary>
        public bool Resolved { set; get; } = false;
    }

    /// <summary>
    /// 应用反馈 检索条件
    /// </summary>
    public class AppFeedbackSearch : BaseProtectedSearch<AppFeedback>
    {

    }
}
