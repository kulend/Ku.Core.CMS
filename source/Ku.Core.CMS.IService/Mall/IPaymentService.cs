//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IPaymentService.cs
// 功能描述：支付方式 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-08 13:31
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IPaymentService : IBaseService<Payment, PaymentDto, PaymentSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(PaymentDto dto);
    }
}
