//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsAccount.cs
// 功能描述：短信账号 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-03-26 16:05
//
//----------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.System;

namespace Ku.Core.CMS.Domain.Entity.System
{
    [Table("system_sms_account")]
    public class SmsAccount : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 账号类型
        /// </summary>
        [DefaultValue(EmSmsAccountType.Aliyun)]
        public EmSmsAccountType Type { set; get; } = EmSmsAccountType.Aliyun;

        /// <summary>
        /// 参数配置
        /// </summary>
        public string ParameterConfig { set; get; }

    }

    public class SmsAccountSearch : BaseProtectedSearch<SmsAccount>
    {

    }
}
