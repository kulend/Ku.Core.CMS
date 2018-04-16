//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppVersion.cs
// 功能描述：应用版本 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-16 10:59
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.DataCenter
{
    [Table("DataCenter_AppVersion")]
    public class AppVersion : BaseProtectedEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public long AppId { set; get; }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual App App { set; get; }

        /// <summary>
        /// 更新版本
        /// </summary>
        [Required, StringLength(20)]
        public string Version { set; get; }

        /// <summary>
        /// 更新内容
        /// </summary>
        [StringLength(2000)]
        public string Content { set; get; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [Required, StringLength(256)]
        public string DownLoadUrl { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 是否强制
        /// </summary>
        public bool Force { set; get; } = false;
    }

    public class AppVersionSearch : BaseSearch<AppVersion>
    {
        public long? AppId { set; get; }
    }
}
