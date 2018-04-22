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

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsDto : BaseProtectedDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        [Display(Name = "手机号")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [StringLength(256)]
        [Display(Name = "短信内容")]
        public string Content { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        [Display(Name = "短信数据")]
        public IDictionary<string, string> Data { set; get; }

        /// <summary>
        /// 短信模板ID
        /// </summary>
        [DataType("Hidden")]
        public long SmsTempletId { set; get; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public SmsTempletDto SmsTemplet { set; get; }
    }
}
