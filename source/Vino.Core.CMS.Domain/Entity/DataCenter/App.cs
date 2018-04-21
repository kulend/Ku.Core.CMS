//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：App.cs
// 功能描述：应用 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.DataCenter;
using Vino.Core.Infrastructure.Attributes;

namespace Vino.Core.CMS.Domain.Entity.DataCenter
{
    [Table("datacenter_app")]
    public class App : BaseProtectedEntity
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        public EmAppType Type { set; get; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required, StringLength(64)]
        public string Name { set; get; }

        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(500)]
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
        public string DownloadUrl { set; get; }

        /// <summary>
        /// 应用KEY
        /// </summary>
        [StringLength(32)]
        public string AppKey { set; get; }
    }

    public class AppSearch : BaseProtectedSearch<App>
    {

    }
}
