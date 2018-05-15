//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AppFeedback.cs
// 功能描述：应用反馈 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.DataCenter
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

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? ResolveTime { set; get; }

        /// <summary>
        /// 管理员备注
        /// </summary>
        [StringLength(1000)]
        public string AdminRemark { set; get; }
    }

    /// <summary>
    /// 应用反馈 检索条件
    /// </summary>
    public class AppFeedbackSearch : BaseProtectedSearch<AppFeedback>
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public long? AppId { set; get; }

        /// <summary>
        /// 是否已解决
        /// </summary>
        public bool? Resolved { set; get; }
    }
}
