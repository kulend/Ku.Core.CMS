//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PageViewRecordDto.cs
// 功能描述：页面浏览记录 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-28 10:37
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.DataCenter
{
    /// <summary>
    /// 页面浏览记录
    /// </summary>
    public class PageViewRecordDto : BaseDto
    {
        [Display(Name = "回话ID")]
        [Required, StringLength(32)]
        public string SessionId { set; get; }

        [Display(Name = "页面名称")]
        [StringLength(32)]
        public string PageName { set; get; }

        [Display(Name = "页面地址")]
        [Required, StringLength(256)]
        public string Url { set; get; }

        [Display(Name = "参照地址")]
        [StringLength(256)]
        public string Referer { set; get; }

        [StringLength(128)]
        public string UserAgent { set; get; }

        [StringLength(64)]
        public string ClientIp { set; get; }

        [StringLength(64)]
        public string Location { set; get; }
    }
}
