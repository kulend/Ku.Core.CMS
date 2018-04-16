//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppVersionDto.cs
// 功能描述：应用版本 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-16 10:59
//
//----------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.DataCenter
{
    public class AppVersionDto : BaseDto
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [DataType("Hidden")]
        public long AppId { set; get; }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual AppDto App { set; get; }

        /// <summary>
        /// 更新版本
        /// </summary>
        [Required, StringLength(20)]
        [Display(Name = "版本号", Description = "采用点分制，如 1.0.2")]
        public string Version { set; get; }

        /// <summary>
        /// 更新内容
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "更新内容")]
        [DataType(DataType.MultilineText)]
        public string Content { set; get; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [Required, StringLength(256)]
        [Display(Name = "下载地址")]
        public string DownLoadUrl { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 是否强制
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "是否强制", Prompt = "是|否")]
        public bool Force { set; get; } = false;
    }
}
