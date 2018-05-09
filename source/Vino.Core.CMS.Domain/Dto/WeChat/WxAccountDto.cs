//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxAccountDto.cs
// 功能描述：公众号 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ku.Core.CMS.Domain.Enum.WeChat;

namespace Ku.Core.CMS.Domain.Dto.WeChat
{
    public class WxAccountDto : BaseProtectedDto
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
