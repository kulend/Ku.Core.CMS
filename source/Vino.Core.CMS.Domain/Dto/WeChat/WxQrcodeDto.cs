using System.ComponentModel.DataAnnotations;
using Vino.Core.CMS.Domain.Enum.WeChat;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.WeChat
{
    public class WxQrcodeDto : BaseDto
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccountDto Account { get; set; }

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
        [Display(Name = "过期时间（秒）")]
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public long? CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public WxUserDto CreateUser { get; set; }

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
}
