//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IOrderProductService.cs
// 功能描述：订单商品 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-30 09:11
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IOrderProductService : IBaseService<OrderProduct, OrderProductDto, OrderProductSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(OrderProductDto dto);
    }
}
