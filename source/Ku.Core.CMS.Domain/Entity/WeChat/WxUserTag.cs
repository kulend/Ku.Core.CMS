//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxUserTag.cs
// 功能描述：微信用户标签 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.WeChat
{
    [Table("wechat_user_tag")]
    public class WxUserTag : BaseEntity
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccount Account { get; set; }

        [Display(Name = "标签ID")]
        public int TagId { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(40)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        [Display(Name = "人数")]
        public int Count { set; get; } = 0;
    }

    public class WxUserTagSearch : BaseProtectedSearch<WxUserTag>
    {
        public long AccountId { set; get; }

        [SearchCondition("Name", SearchConditionOperation.Contains)]
        public string Keyword { set; get; }
    }
}
