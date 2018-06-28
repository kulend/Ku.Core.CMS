//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：BrandDto.cs
// 功能描述：品牌 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-26 11:09
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Mall
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class BrandDto : BaseProtectedDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(128)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// LOGO
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "LOGO")]
        public string Logo { set; get; }


        /// <summary>
        /// 介绍
        /// </summary>
        [MaxLength(1024)]
        [Display(Name = "介绍")]
        [DataType(DataType.MultilineText)]
        public string Intro { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; }

        public virtual IEnumerable<ProductCategoryDto> Categorys { set; get; }
    }
}
