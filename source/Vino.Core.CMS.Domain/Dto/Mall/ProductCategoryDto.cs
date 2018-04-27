//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductCategoryDto.cs
// 功能描述：商品类目 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-26 11:32
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vino.Core.CMS.Domain.Dto.Mall
{
    /// <summary>
    /// 商品类目
    /// </summary>
    public class ProductCategoryDto : BaseProtectedDto
    {
        /// <summary>
        /// 父类目
        /// </summary>
        [DataType("Hidden")]
        public long? ParentId { get; set; }

        /// <summary>
        /// 父类目
        /// </summary>
        public ProductCategoryDto Parent { get; set; }

        /// <summary>
        /// 类目名称
        /// </summary>
        [Required, StringLength(40)]
        [Display(Name = "类目名称")]
        public string Name { set; get; }

        /// <summary>
        /// 层级
        /// </summary>
        [Display(Name = "层级")]
        public int Level { set; get; } = 1;
    }
}
