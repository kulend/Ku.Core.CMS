//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsQueue.cs
// 功能描述：短信队列 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Domain.Enum.System;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_sms_queue")]
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

        public Sms Sms { set; get; }

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

    public class SmsQueueSearch : BaseProtectedSearch<SmsQueue>
    {
        public EmSmsQueueStatus? Status { set; get; }
    }
}
