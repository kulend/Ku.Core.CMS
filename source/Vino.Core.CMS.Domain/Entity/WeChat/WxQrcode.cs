//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxQrcode.cs
// 功能描述：微信二维码 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.WeChat;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.WeChat
{
    [Table("wechat_qrcode")]
    public class WxQrcode : BaseProtectedEntity
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccount Account { get; set; }

        /// <summary>
        /// 场景ID
        /// </summary>
        [Display(Name = "场景ID")]
        public int SceneId { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        [Display(Name = "场景类型")]
        public EmWxSceneType SceneType { get; set; }

        /// <summary>
        /// 时效类型
        /// </summary>
        [Display(Name = "时效类型")]
        public EmWxPeriodType PeriodType { get; set; }

        /// <summary>
        /// 过期时间（秒）
        /// </summary>
        [Display(Name = "过期时间", Description = "秒，最大不超过2592000（即30天）")]
        [Range(0, 2592000)]
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public long? CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public WxUser CreateUser { get; set; }

        /// <summary>
        /// Ticket
        /// </summary>
        [Display(Name = "Ticket")]
        [MaxLength(256)]
        public string Ticket { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Display(Name = "Url")]
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 事件Key
        /// </summary>
        [Display(Name = "事件")]
        [MaxLength(64)]
        public string EventKey { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        [MaxLength(128)]
        [Display(Name = "用途")]
        public string Purpose { get; set; }

        /// <summary>
        /// 扫描次数
        /// </summary>
        [Display(Name = "扫描次数")]
        public int ScanCount { get; set; }

    }

    public class WxQrcodeSearch : BaseSearch<WxQrcode>
    {
        public long AccountId { set; get; }
    }
}
