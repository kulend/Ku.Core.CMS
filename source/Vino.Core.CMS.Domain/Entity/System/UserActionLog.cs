//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：UserActionLog.cs
// 功能描述：用户操作日志 实体类
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

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_action_log")]
    public class UserActionLog : BaseEntity
    {
        [MaxLength(40)]
        public string Operation { set; get; }

        [MaxLength(30)]
        public string ControllerName { set; get; }

        [MaxLength(30)]
        public string ActionName { set; get; }

        public long? UserId { set; get; }

        [MaxLength(256)]
        public string Url { set; get; }

        [MaxLength(256)]
        public string UrlReferrer { set; get; }

        [MaxLength(46)]
        public string Ip { set; get; }

        [MaxLength(500)]
        public string ActionResult { set; get; }

        public User User { set; get; }

        [MaxLength(256)]
        public string UserAgent { set; get; }

        [MaxLength(10)]
        public string Method { set; get; }

        [MaxLength(256)]
        public string QueryString { set; get; }
    }

    public class UserActionLogSearch : BaseProtectedSearch<UserActionLog>
    {

    }
}
