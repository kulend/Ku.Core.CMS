using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.WeChat
{
    public class WxUserTagDto : BaseDto
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccountDto Account { get; set; }

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

        public string Keyword { set; get; }
    }
}
