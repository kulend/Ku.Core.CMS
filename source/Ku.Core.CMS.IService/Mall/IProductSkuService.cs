//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IProductSkuService.cs
// 功能描述：商品SKU 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IProductSkuService : IBaseService<ProductSku, ProductSkuDto, ProductSkuSearch>
    {
    }
}
