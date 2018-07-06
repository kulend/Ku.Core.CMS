//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MaskWordDto.cs
// 功能描述：屏蔽关键词 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-03 14:22
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Content;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 屏蔽关键词
    /// </summary>
    public class MaskWordDto : BaseDto
    {
        /// <summary>
        /// 等级
        /// </summary>
        [Display(Name = "等级")]
        public EmMaskWordLevel Level { set; get; }

        /// <summary>
        /// 关键词
        /// </summary>
        [Required, StringLength(64)]
        [Display(Name = "关键词")]
        public string Word { set; get; }

        /// <summary>
        /// 标签
        /// </summary>
        [Display(Name = "标签")]
        [StringLength(128)]
        public string Tag { set; get; }

        /// <summary>
        /// 匹配次数
        /// </summary>
        [Display(Name = "匹配次数")]
        public int MatchCount { set; get; }
    }
}
