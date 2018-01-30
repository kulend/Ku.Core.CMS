using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.WeChat
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

    public class WxUserTagSearch : BaseSearch<WxUserTag>
    {
        public long AccountId { set; get; }

        [SearchCondition("Name", SearchConditionOperation.Contains)]
        public string Keyword { set; get; }
    }
}
