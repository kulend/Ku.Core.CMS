//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMemberPointService.cs
// 功能描述：会员积分 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.Domain.Enum.Membership;

namespace Ku.Core.CMS.IService.Membership
{
    public partial interface IMemberPointService : IBaseService<MemberPoint, MemberPointDto, MemberPointSearch>
    {
        /// <summary>
        /// 获得积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        Task GainAsync(long MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId);

        /// <summary>
        /// 消费积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        Task ConsumeAsync(long MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId);

        /// <summary>
        /// 调整积分
        /// </summary>
        Task AdjustAsync(long[] MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId);
    }
}
