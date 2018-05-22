//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsAccountDto.cs
// 功能描述：短信账户 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:11
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Communication;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Communication
{
    /// <summary>
    /// 短信账户
    /// </summary>
    public class SmsAccountDto : BaseProtectedDto
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
        [DataType(dataType: DataType.MultilineText)]
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
