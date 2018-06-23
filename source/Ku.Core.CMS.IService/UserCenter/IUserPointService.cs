//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserPointService.cs
// 功能描述：用户积分 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 10:50
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.Domain.Enum.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserPointService : IBaseService<UserPoint, UserPointDto, UserPointSearch>
    {
        /// <summary>
        /// 获得积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        Task GainAsync(long UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId);

        /// <summary>
        /// 消费积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        Task ConsumeAsync(long UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId);

        /// <summary>
        /// 调整积分
        /// </summary>
        Task AdjustAsync(long[] UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId);
    }
}
