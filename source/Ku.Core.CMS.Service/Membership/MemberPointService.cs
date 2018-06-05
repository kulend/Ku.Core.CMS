//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberPointService.cs
// 功能描述：会员积分 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.Domain.Enum.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberPointService : BaseService<MemberPoint, MemberPointDto, MemberPointSearch>, IMemberPointService
    {
        /// <summary>
        /// 获得积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        public async Task GainAsync(long MemberId, EmMemberPointType MemberPointType, 
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                try
                {
                    await _GainAsync(dapper, MemberId, MemberPointType, Points, BusType, BusId, BusDesc, OperatorId);
                    dapper.Commit();
                }
                catch (Exception)
                {
                    dapper.Rollback();
                    throw;
                }
            }
        }

        private async Task _GainAsync(IDapper dapper, long MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new KuArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new KuArgNullException("调整积分超出范围！");
            }

            //取得会员数据
            var member = await dapper.QueryOneAsync<Member>(new { Id = MemberId });
            if (member == null)
            {
                throw new KuArgNullException("无法取得会员数据！");
            }

            //取得会员积分数据
            var memberPoint = await dapper.QueryOneAsync<MemberPoint>(new { MemberId = MemberId, Type = MemberPointType, IsDeleted = false });
            if (memberPoint == null)
            {
                //创建新会员积分数据
                memberPoint = new MemberPoint
                {
                    Id = ID.NewID(),
                    MemberId = MemberId,
                    Type = MemberPointType,
                    Points = Points,
                    ExpiredPoints = 0,
                    UsablePoints = Points,
                    UsedPoints = 0,
                    IsDeleted = false,
                    CreateTime = DateTime.Now
                };
                await dapper.InsertAsync(memberPoint);
            }
            else
            {
                //积分计算
                var data = new
                {
                    UsablePoints = memberPoint.UsablePoints + Points,
                    Points = memberPoint.Points + Points
                };

                await dapper.UpdateAsync<MemberPoint>(data, new { Id = memberPoint.Id });
            }

            //log
            MemberPointRecord record = new MemberPointRecord
            {
                Id = ID.NewID(),
                MemberPointId = memberPoint.Id,
                MemberId = MemberId,
                ChangeType = EmMemberPointChangeType.Gain,
                Points = Points,
                BusType = BusType,
                BusId = BusId,
                BusDesc = BusDesc,
                OperatorId = OperatorId,
                IsDeleted = false,
                CreateTime = DateTime.Now
            };
            await dapper.InsertAsync(record);
        }

        /// <summary>
        /// 消费积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        public async Task ConsumeAsync(long MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                try
                {
                    await _ConsumeAsync(dapper, MemberId, MemberPointType, Points, BusType, BusId, BusDesc, OperatorId);
                    dapper.Commit();
                }
                catch (Exception)
                {
                    dapper.Rollback();
                    throw;
                }
            }
        }

        private async Task _ConsumeAsync(IDapper dapper, long MemberId, EmMemberPointType MemberPointType,
                int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new KuArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new KuArgNullException("调整积分超出范围！");
            }
            //取得会员数据
            var member = await dapper.QueryOneAsync<Member>(new { Id = MemberId });
            if (member == null)
            {
                throw new KuArgNullException("无法取得会员数据！");
            }

            //取得会员积分数据
            var memberPoint = await dapper.QueryOneAsync<MemberPoint>(new { MemberId = MemberId, Type = MemberPointType, IsDeleted = false });
            if (memberPoint == null)
            {
                throw new KuArgNullException($"会员[{member.Name}]积分不足！");
            }
            else
            {
                if (memberPoint.UsablePoints - Points < 0)
                {
                    throw new KuArgNullException($"会员[{member.Name}]积分不足！");
                }

                //积分计算
                var data = new
                {
                    UsablePoints = memberPoint.UsablePoints - Points,
                    UsedPoints = memberPoint.UsedPoints + Points
                };

                await dapper.UpdateAsync<MemberPoint>(data, new { Id = memberPoint.Id });
            }

            //log
            MemberPointRecord record = new MemberPointRecord
            {
                Id = ID.NewID(),
                MemberPointId = memberPoint.Id,
                MemberId = MemberId,
                ChangeType = EmMemberPointChangeType.Consume,
                Points = Points,
                BusType = BusType,
                BusId = BusId,
                BusDesc = BusDesc,
                OperatorId = OperatorId,
                IsDeleted = false,
                CreateTime = DateTime.Now
            };

            await dapper.InsertAsync(record);
        }

        /// <summary>
        /// 调整积分
        /// </summary>
        public async Task AdjustAsync(long[] MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (MemberId == null || MemberId.Length == 0)
            {
                throw new KuArgNullException("请选择会员！");
            }
            if (Points == 0)
            {
                throw new KuArgNullException("调整积分不能为0！");
            }
            if (Points < -9999 || Points > 9999)
            {
                throw new KuArgNullException("调整积分超出范围！");
            }

            using (var dapper = DapperFactory.CreateWithTrans())
            {
                try
                {
                    if (Points > 0)
                    {
                        foreach (var id in MemberId)
                        {
                            await _GainAsync(dapper, id, MemberPointType, Points, BusType, BusId, BusDesc, OperatorId);
                        }
                    }
                    else
                    {
                        foreach (var id in MemberId)
                        {
                            await _ConsumeAsync(dapper, id, MemberPointType, Math.Abs(Points), BusType, BusId, BusDesc, OperatorId);
                        }
                    }
                    dapper.Commit();
                }
                catch (Exception)
                {
                    dapper.Rollback();
                    throw;
                }
            }
        }
    }
}
