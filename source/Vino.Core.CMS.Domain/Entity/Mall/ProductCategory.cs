//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductCategory.cs
// 功能描述：商品类目 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-26 11:32
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 商品类目
    /// </summary>
    [Table("mall_product_category")]
    public class ProductCategory : BaseProtectedEntity
    {
        /// <summary>
        /// 父类目
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 父类目
        /// </summary>
        public ProductCategory Parent { get; set; }

        [Required, StringLength(40)]
        public string Name { set; get; }

    }

    /// <summary>
    /// 商品类目 检索条件
    /// </summary>
    public class ProductCategorySearch : BaseProtectedSearch<ProductCategory>
    {
        /// <summary>
        /// 父类目
        /// </summary>
        [SearchCondition(ignoreWhenNull: false)]
        public long? ParentId { get; set; }
    }
}
