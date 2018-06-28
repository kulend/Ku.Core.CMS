//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：BrandCategoryRef.cs
// 功能描述：品牌类目关联 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-26 16:16
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Mall
{
    /// <summary>
    /// 品牌类目关联
    /// </summary>
    [Table("mall_brand_category_ref")]
    public class BrandCategoryRef
    {
        public long BrandId { set; get; }

        public long ProductCategoryId { set; get; }
    }

}
