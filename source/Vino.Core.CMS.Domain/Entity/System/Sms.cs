//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Sms.cs
// 功能描述：短信 实体类
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
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_sms")]
    public class Sms : BaseProtectedEntity
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
        /// 签名
        /// </summary>
        [MaxLength(40)]
        public string Signature { set; get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { set; get; }
    }

    public class SmsSearch : BaseSearch<Sms>
    {

    }
}
