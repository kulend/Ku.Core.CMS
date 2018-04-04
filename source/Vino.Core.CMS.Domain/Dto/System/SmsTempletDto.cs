//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsTempletDto.cs
// 功能描述：短信模板 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-02 09:50
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsTempletDto : BaseDto
    {
        /// <summary>
        /// 标记
        /// </summary>
        [Display(Name = "标记")]
        [Required, StringLength(64, MinimumLength = 5)]
        public string Tag { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required, StringLength(100)]
        public string Name { set; get; }

        /// <summary>
        /// 示例
        /// </summary>
        [Display(Name = "示例")]
        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string Example { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        [Display(Name = "模板")]
        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string Templet { get; set; }

        /// <summary>
        /// 模板KEY
        /// </summary>
        [Display(Name = "模板KEY")]
        [StringLength(64)]
        public string TempletKey { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [Display(Name = "签名")]
        [StringLength(40)]
        public string Signature { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;
    }
}
