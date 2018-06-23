//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserPointService.cs
// 功能描述：用户积分 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 10:50
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.Domain.Enum.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class UserPointService : BaseService<UserPoint, UserPointDto, UserPointSearch>, IUserPointService
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public override async Task<(int count, List<UserPointDto> items)> GetListAsync(int page, int size, UserPointSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryPageAsync<UserPoint, User, UserPoint>(page, size, "t1.*,t2.*",
                    "usercenter_user_point t1 INNER JOIN usercenter_user t2 ON t1.UserId=t2.Id",
                    where.ParseToDapperSql(dapper.Dialect, "t1"), sort as object, (t1, t2) =>
                    {
                        t1.User = t2;
                        return t1;
                    }, splitOn: "Id");
                return (data.count, Mapper.Map<List<UserPointDto>>(data.items));
            }
        }

        /// <summary>
        /// 获得积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        public async Task GainAsync(long UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                try
                {
                    await _GainAsync(dapper, UserId, UserPointType, Points, BusType, BusId, BusDesc, OperatorId);
                    dapper.Commit();
                }
                catch (Exception)
                {
                    dapper.Rollback();
                    throw;
                }
            }
        }

        private async Task _GainAsync(IDapper dapper, long UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new KuArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new KuArgNullException("调整积分超出范围！");
            }

            //取得用户数据
            var user = await dapper.QueryOneAsync<User>(new { Id = UserId });
            if (user == null)
            {
                throw new KuArgNullException("无法取得用户数据！");
            }

            //取得用户积分数据
            var userPoint = await dapper.QueryOneAsync<UserPoint>(new { UserId = UserId, Type = UserPointType, IsDeleted = false });
            if (userPoint == null)
            {
                //创建新用户积分数据
                userPoint = new UserPoint
                {
                    Id = ID.NewID(),
                    UserId = UserId,
                    Type = UserPointType,
                    Points = Points,
                    ExpiredPoints = 0,
                    UsablePoints = Points,
                    UsedPoints = 0,
                    IsDeleted = false,
                    CreateTime = DateTime.Now
                };
                await dapper.InsertAsync(userPoint);
            }
            else
            {
                //积分计算
                var data = new
                {
                    UsablePoints = userPoint.UsablePoints + Points,
                    Points = userPoint.Points + Points
                };

                await dapper.UpdateAsync<UserPoint>(data, new { Id = userPoint.Id });
            }

            //log
            UserPointRecord record = new UserPointRecord
            {
                Id = ID.NewID(),
                UserPointId = userPoint.Id,
                UserId = UserId,
                ChangeType = EmUserPointChangeType.Gain,
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
        public async Task ConsumeAsync(long UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                try
                {
                    await _ConsumeAsync(dapper, UserId, UserPointType, Points, BusType, BusId, BusDesc, OperatorId);
                    dapper.Commit();
                }
                catch (Exception)
                {
                    dapper.Rollback();
                    throw;
                }
            }
        }

        private async Task _ConsumeAsync(IDapper dapper, long UserId, EmUserPointType UserPointType,
                int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new KuArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new KuArgNullException("调整积分超出范围！");
            }
            //取得用户数据
            var user = await dapper.QueryOneAsync<User>(new { Id = UserId });
            if (user == null)
            {
                throw new KuArgNullException("无法取得用户数据！");
            }

            //取得用户积分数据
            var userPoint = await dapper.QueryOneAsync<UserPoint>(new { UserId = UserId, Type = UserPointType, IsDeleted = false });
            if (userPoint == null)
            {
                throw new KuArgNullException($"用户[{user.NickName}]积分不足！");
            }
            else
            {
                if (userPoint.UsablePoints - Points < 0)
                {
                    throw new KuArgNullException($"用户[{user.NickName}]积分不足！");
                }

                //积分计算
                var data = new
                {
                    UsablePoints = userPoint.UsablePoints - Points,
                    UsedPoints = userPoint.UsedPoints + Points
                };

                await dapper.UpdateAsync<UserPoint>(data, new { Id = userPoint.Id });
            }

            //log
            UserPointRecord record = new UserPointRecord
            {
                Id = ID.NewID(),
                UserPointId = userPoint.Id,
                UserId = UserId,
                ChangeType = EmUserPointChangeType.Consume,
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
        public async Task AdjustAsync(long[] UserId, EmUserPointType UserPointType,
            int Points, EmUserPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (UserId == null || UserId.Length == 0)
            {
                throw new KuArgNullException("请选择用户！");
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
                        foreach (var id in UserId)
                        {
                            await _GainAsync(dapper, id, UserPointType, Points, BusType, BusId, BusDesc, OperatorId);
                        }
                    }
                    else
                    {
                        foreach (var id in UserId)
                        {
                            await _ConsumeAsync(dapper, id, UserPointType, Math.Abs(Points), BusType, BusId, BusDesc, OperatorId);
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
