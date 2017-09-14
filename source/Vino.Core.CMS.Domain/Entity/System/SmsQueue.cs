using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Domain.Enum.System;
using Vino.Core.Infrastructure.Data;

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
        /// 发送时间
        /// </summary>
        public DateTime SendTime { set; get; }
    }
}
