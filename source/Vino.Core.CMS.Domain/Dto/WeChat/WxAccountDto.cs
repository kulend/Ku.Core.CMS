﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Enum.WeChat;

namespace Vino.Core.CMS.Domain.Dto.WeChat
{
    public class WxAccountDto : BaseDto
    {
        [Required, MaxLength(40)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        [Display(Name = "类型")]
        public EmWxAccountType Type { set; get; }

        [MaxLength(50)]
        [Display(Name = "原始ID")]
        public string OriginalId { set; get; }

        [MaxLength(128)]
        [Display(Name = "微信号")]
        public string WeixinId { set; get; }

        [MaxLength(256)]
        [Display(Name = "头像")]
        public string Image { set; get; }

        [MaxLength(32)]
        [Display(Name = "应用ID")]
        public string AppId { set; get; }

        [MaxLength(512)]
        [Display(Name = "应用密钥")]
        public string AppSecret { set; get; }

        [MaxLength(30)]
        [Display(Name = "令牌")]
        public string Token { set; get; }
    }
}
