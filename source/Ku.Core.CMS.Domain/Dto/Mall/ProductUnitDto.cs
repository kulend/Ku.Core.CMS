//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ProductUnitDto.cs
// 功能描述：计量单位 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-28 17:28
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Mall
{
    /// <summary>
    /// 计量单位
    /// </summary>
    public class ProductUnitDto : BaseProtectedDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }
    }
}
