//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsQueue.cs
// 功能描述：短信队列 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:57
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Communication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Communication
{
    /// <summary>
    /// 短信队列
    /// </summary>
    [Table("communication_sms_queue")]
    public class SmsQueue : BaseProtectedEntity
    {
        /// <summary>
        /// 状态
        /// </summary>
        public EmSmsQueueStatus Status { set; get; }

        /// <summary>
        /// 短信ID
        /// </summary>
        public long SmsId { set; get; }

        public virtual Sms Sms { set; get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { set; get; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { set; get; }

        [StringLength(2000)]
        public string Response { set; get; }
    }

    /// <summary>
    /// 短信队列 检索条件
    /// </summary>
    public class SmsQueueSearch : BaseProtectedSearch<SmsQueue>
    {
        public EmSmsQueueStatus? Status { set; get; }
    }
}
