//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ProductSkuService.cs
// 功能描述：商品SKU 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;

namespace Ku.Core.CMS.Service.Mall
{
    public partial class ProductSkuService : BaseService<ProductSku, ProductSkuDto, ProductSkuSearch>, IProductSkuService
    {
    }
}
