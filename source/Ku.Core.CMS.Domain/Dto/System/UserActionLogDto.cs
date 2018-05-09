//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：UserActionLogDto.cs
// 功能描述：用户操作日志 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ku.Core.EventBus;

namespace Ku.Core.CMS.Domain.Dto.System
{
    [Event("backend_user_action_log")]
    public class UserActionLogDto : BaseProtectedDto
    {
        [Display(Name = "操作说明")]
        [MaxLength(40)]
        public string Operation { set; get; }

        [MaxLength(60)]
        [Display(Name = "Controller")]
        public string ControllerName { set; get; }

        [MaxLength(30)]
        [Display(Name = "Action")]
        public string ActionName { set; get; }

        public long? UserId { set; get; }

        [MaxLength(256)]
        [Display(Name = "Url")]
        public string Url { set; get; }

        [MaxLength(256)]
        [Display(Name = "UrlReferrer")]
        public string UrlReferrer { set; get; }

        [MaxLength(46)]
        [Display(Name = "IP")]
        public string Ip { set; get; }

        [MaxLength(500)]
        public string ActionResult { set; get; }

        [Display(Name = "操作说明")]
        public string UserName { set; get; }

        public UserDto User { set; get; }

        [MaxLength(256)]
        [Display(Name = "UserAgent")]
        public string UserAgent { set; get; }

        [MaxLength(10)]
        [Display(Name = "Method")]
        public string Method { set; get; }

        [MaxLength(256)]
        [Display(Name = "QueryString")]
        public string QueryString { set; get; }

        [Display(Name = "操作时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public new DateTime CreateTime { set; get; }
    }
}
