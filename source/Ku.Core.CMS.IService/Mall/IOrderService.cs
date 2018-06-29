//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IOrderService.cs
// 功能描述：订单 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-29 15:51
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IOrderService : IBaseService<Order, OrderDto, OrderSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(OrderDto dto);
    }
}
