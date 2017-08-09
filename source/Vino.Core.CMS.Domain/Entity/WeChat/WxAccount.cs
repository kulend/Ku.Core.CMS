using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Enum.WeChat;

namespace Vino.Core.CMS.Domain.Entity.WeChat
{
    [Table("wechat_account")]
    public class WxAccount : BaseProtectedEntity
    {
        [Required, MaxLength(40)]
        public string Name { set; get; }

        public EmWxAccountType Type { set; get; }

        [MaxLength(50)]
        public string OriginalId { set; get; }

        [MaxLength(128)]
        public string WeixinId { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(32)]
        public string AppId { set; get; }

        [MaxLength(512)]
        public string AppSecret { set; get; }

        [MaxLength(30)]
        public string Token { set; get; }
    }
}
