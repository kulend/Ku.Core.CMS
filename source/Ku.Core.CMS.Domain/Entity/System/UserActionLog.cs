//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserActionLog.cs
// 功能描述：用户操作记录 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Entity.UserCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ku.Core.CMS.Domain.Entity.System
{
    /// <summary>
    /// 用户操作记录
    /// </summary>
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

    /// <summary>
    /// 用户操作记录 检索条件
    /// </summary>
    public class UserActionLogSearch : BaseSearch<UserActionLog>
    {

    }
}
