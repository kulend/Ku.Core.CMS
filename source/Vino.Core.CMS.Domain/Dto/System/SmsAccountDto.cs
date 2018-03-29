//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsAccountDto.cs
// 功能描述：短信账号 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-03-26 16:05
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.System;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsAccountDto : BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required, StringLength(40)]
        public string Name { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        [Display(Name = "备注")]
        [DataType(dataType:DataType.MultilineText)]
        public string Remarks { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 账号类型
        /// </summary>
        [DefaultValue(EmSmsAccountType.Aliyun)]
        [Display(Name = "账号类型")]
        public EmSmsAccountType Type { set; get; } = EmSmsAccountType.Aliyun;

        /// <summary>
        /// 参数配置
        /// </summary>
        [Display(Name = "参数配置")]
        public IDictionary<string, string> ParameterConfig { set; get; }
    }
}
