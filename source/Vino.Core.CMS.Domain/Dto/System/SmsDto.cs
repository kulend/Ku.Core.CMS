//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsDto.cs
// 功能描述：短信 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsDto : BaseDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [MaxLength(256)]
        public string Content { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        [MaxLength(512)]
        public string Data { set; get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { set; get; }

        /// <summary>
        /// 短信模板ID
        /// </summary>
        public long SmsTempletId { set; get; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public SmsTempletDto SmsTemplet { set; get; }
    }
}
