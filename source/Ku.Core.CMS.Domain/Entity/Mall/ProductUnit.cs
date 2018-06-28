//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ProductUnit.cs
// 功能描述：计量单位 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-28 17:28
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 计量单位
    /// </summary>
    [Table("mall_product_unit")]
    public class ProductUnit : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }
    }

    /// <summary>
    /// 计量单位 检索条件
    /// </summary>
    public class ProductUnitSearch : BaseProtectedSearch<ProductUnit>
    {

    }
}
