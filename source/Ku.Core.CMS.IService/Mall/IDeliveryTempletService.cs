//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IDeliveryTempletService.cs
// 功能描述：配送模板 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IDeliveryTempletService : IBaseService<DeliveryTemplet, DeliveryTempletDto, DeliveryTempletSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(DeliveryTempletDto dto);
    }
}
