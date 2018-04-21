//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxUserTagDto.cs
// 功能描述：微信用户标签 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

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
}
