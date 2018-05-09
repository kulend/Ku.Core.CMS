//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppDto.cs
// 功能描述：应用 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.DataCenter;

namespace Ku.Core.CMS.Domain.Dto.DataCenter
{
    public class AppDto : BaseProtectedDto
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        [Display(Name = "应用类型")]
        public EmAppType Type { set; get; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required, StringLength(64)]
        [Display(Name = "应用名称")]
        public string Name { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(500)]
        [Display(Name = "应用简介")]
        public string Intro { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "图标")]
        public string IconData { set; get; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "下载地址")]
        public string DownloadUrl { set; get; }

        /// <summary>
        /// 应用KEY
        /// </summary>
        [StringLength(32)]
        [Display(Name = "应用KEY")]
        public string AppKey { set; get; }
    }
}
