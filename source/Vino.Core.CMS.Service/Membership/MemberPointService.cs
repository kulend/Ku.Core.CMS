//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberPointService.cs
// 功能描述：会员积分 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.Membership;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.Domain.Enum.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberPointService : BaseService, IMemberPointService
    {
        protected readonly IMemberPointRepository _repository;
        protected readonly IMemberRepository _memberRepository;
        protected readonly IMemberPointRecordRepository _recordRepository;

        #region 构造函数

        public MemberPointService(IMemberPointRepository repository, 
            IMemberRepository memberRepository, IMemberPointRecordRepository recordRepository)
        {
            this._repository = repository;
            this._memberRepository = memberRepository;
            this._recordRepository = recordRepository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MemberPointDto></returns>
        public async Task<List<MemberPointDto>> GetListAsync(MemberPointSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<MemberPointDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MemberPointDto> items)> GetListAsync(int page, int size, MemberPointSearch where, string sort)
        {
            var includes = new List<Expression<Func<MemberPoint, object>>>();
            includes.Add(x => x.Member);
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc", includes.ToArray());
            return (data.count, Mapper.Map<List<MemberPointDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MemberPointDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MemberPointDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberPointDto dto)
        {
            MemberPoint model = Mapper.Map<MemberPoint>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员积分数据！");
                }

                //TODO:这里进行赋值

                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(params long[] id)
        {
            if (await _repository.DeleteAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task RestoreAsync(params long[] id)
        {
            if (await _repository.RestoreAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        #endregion

        #region 其他方法

        /// <summary>
        /// 获得积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        public async Task GainAsync(long MemberId, EmMemberPointType MemberPointType, 
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new VinoArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new VinoArgNullException("调整积分超出范围！");
            }

            //取得会员数据
            var member = await _memberRepository.GetByIdAsync(MemberId);
            if (member == null)
            {
                throw new VinoArgNullException("无法取得会员数据！");
            }

            //取得会员积分数据
            var memberPoint = await _repository.FirstOrDefaultAsync(x => x.MemberId == MemberId && x.Type == MemberPointType && !x.IsDeleted);
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

                await _repository.InsertAsync(memberPoint);
            }
            else
            {
                //积分计算
                memberPoint.UsablePoints = memberPoint.UsablePoints + Points;
                memberPoint.Points = memberPoint.Points + Points;

                _repository.Update(memberPoint);
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

            await _recordRepository.InsertAsync(record);
        }

        /// <summary>
        /// 消费积分
        /// </summary>
        /// <remarks>请在Transaction中使用</remarks>
        /// <returns></returns>
        public async Task ConsumeAsync(long MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (Points <= 0)
            {
                throw new VinoArgNullException("积分值必须大于0！");
            }
            if (Points > 9999)
            {
                throw new VinoArgNullException("调整积分超出范围！");
            }

            //取得会员数据
            var member = await _memberRepository.GetByIdAsync(MemberId);
            if (member == null)
            {
                throw new VinoArgNullException("无法取得会员数据！");
            }

            //取得会员积分数据
            var memberPoint = await _repository.FirstOrDefaultAsync(x => x.MemberId == MemberId && x.Type == MemberPointType && !x.IsDeleted);
            if (memberPoint == null)
            {
                throw new VinoArgNullException($"会员[{member.Name}]积分不足！");
            }
            else
            {
                if (memberPoint.UsablePoints - Points < 0)
                {
                    throw new VinoArgNullException($"会员[{member.Name}]积分不足！");
                }

                //积分计算
                memberPoint.UsablePoints = memberPoint.UsablePoints - Points;
                memberPoint.UsedPoints = memberPoint.UsedPoints + Points;

                _repository.Update(memberPoint);
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

            await _recordRepository.InsertAsync(record);
        }

        /// <summary>
        /// 调整积分
        /// </summary>
        public async Task AdjustAsync(long[] MemberId, EmMemberPointType MemberPointType,
            int Points, EmMemberPointBusType BusType, long BusId, string BusDesc, long? OperatorId)
        {
            if (MemberId == null || MemberId.Length == 0)
            {
                throw new VinoArgNullException("请选择会员！");
            }
            if (Points == 0)
            {
                throw new VinoArgNullException("调整积分不能为0！");
            }
            if (Points < -9999 || Points > 9999)
            {
                throw new VinoArgNullException("调整积分超出范围！");
            }

            using (var trans = await _repository.BeginTransactionAsync())
            {
                try
                {
                    if (Points > 0)
                    {
                        foreach (var id in MemberId)
                        {
                            await GainAsync(id, MemberPointType, Points, BusType, BusId, BusDesc, OperatorId);
                        }
                    }
                    else
                    {
                        foreach (var id in MemberId)
                        {
                            await ConsumeAsync(id, MemberPointType, Math.Abs(Points), BusType, BusId, BusDesc, OperatorId);
                        }
                    }
                    await _repository.SaveAsync();
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion
    }
}
