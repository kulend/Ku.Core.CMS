//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsQueueDto.cs
// 功能描述：短信队列 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Enum.System;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsQueueDto : BaseProtectedDto
    {
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EmSmsQueueStatus Status { set; get; }

        /// <summary>
        /// 短信ID
        /// </summary>
        public long SmsId { set; get; }

        public SmsDto Sms { set; get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Display(Name = "过期时间")]
        public DateTime ExpireTime { set; get; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Display(Name = "发送时间")]
        public DateTime? SendTime { set; get; }

        [StringLength(2000)]
        [Display(Name = "发送回应")]
        public string Response { set; get; }
    }
}
