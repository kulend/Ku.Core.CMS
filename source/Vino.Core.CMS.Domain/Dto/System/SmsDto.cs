using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class SmsDto : BaseDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [MaxLength(256)]
        public string Content { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        [MaxLength(512)]
        public string Data { set; get; }

        /// <summary>
        /// 签名
        /// </summary>
        [MaxLength(40)]
        public string Signature { set; get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { set; get; }
    }
}
