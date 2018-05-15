//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxAccount.cs
// 功能描述：微信账户 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Ku.Core.CMS.Domain.Enum.WeChat;

namespace Ku.Core.CMS.Domain.Entity.WeChat
{
    /// <summary>
    /// 微信账户
    /// </summary>
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

    /// <summary>
    /// 微信账户 检索条件
    /// </summary>
    public class WxAccountSearch : BaseProtectedSearch<WxAccount>
    {

    }
}
