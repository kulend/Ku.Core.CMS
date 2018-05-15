//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IProductCategoryService.cs
// 功能描述：商品类目 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-26 11:32
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IProductCategoryService : IBaseService<ProductCategory, ProductCategoryDto, ProductCategorySearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(ProductCategoryDto dto);

        /// <summary>
        /// 根据数据（包含所有父级数据）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<ProductCategoryDto>> GetWithParentsAsync(long id);

        Task<object> GetJsonDataAsync();
    }
}
