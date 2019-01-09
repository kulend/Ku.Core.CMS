//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PageViewRecord.cs
// 功能描述：页面浏览记录 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-28 10:37
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.DataCenter
{
    /// <summary>
    /// 页面浏览记录
    /// </summary>
    [Table("datacenter_pageview_record")]
    public class PageViewRecord : BaseEntity
    {
        [Required, StringLength(64)]
        public string SessionId { set; get; }

        [StringLength(32)]
        public string PageName { set; get; }

        [Required, StringLength(256)]
        public string Url { set; get; }

        [StringLength(256)]
        public string Referer { set; get; }

        [StringLength(128)]
        public string UserAgent { set; get; }

        [StringLength(64)]
        public string ClientIp { set; get; }

        [StringLength(128)]
        public string Location { set; get; }
    }

    /// <summary>
    /// 页面浏览记录 检索条件
    /// </summary>
    public class PageViewRecordSearch : BaseSearch<PageViewRecord>
    {

    }
}
