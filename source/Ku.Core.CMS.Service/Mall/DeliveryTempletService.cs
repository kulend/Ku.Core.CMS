//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：DeliveryTempletService.cs
// 功能描述：配送模板 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.Mall;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;

namespace Ku.Core.CMS.Service.Mall
{
    public partial class DeliveryTempletService : BaseService, IDeliveryTempletService
    {
        protected readonly IDeliveryTempletRepository _repository;

        #region 构造函数

        public DeliveryTempletService(IDeliveryTempletRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<DeliveryTempletDto></returns>
        public async Task<List<DeliveryTempletDto>> GetListAsync(DeliveryTempletSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<DeliveryTempletDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<DeliveryTempletDto> items)> GetListAsync(int page, int size, DeliveryTempletSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<DeliveryTempletDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<DeliveryTempletDto> GetByIdAsync(long id)
        {
            return Mapper.Map<DeliveryTempletDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(DeliveryTempletDto dto)
        {
            DeliveryTemplet model = Mapper.Map<DeliveryTemplet>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.IsSnapshot = false;
                model.SnapshotCount = 0;
                model.OriginId = null;
                model.EffectiveTime = DateTime.Now;

                await _repository.InsertAsync(model);
                await _repository.SaveAsync();
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得配送模板数据！");
                }

                using (var trans = await _repository.BeginTransactionAsync())
                {
                    //生成快照
                    if (item.ChargeMode != model.ChargeMode
                        || !item.ChargeConfig.Equals(model.ChargeConfig))
                    {
                        DeliveryTemplet snapshot = new DeliveryTemplet();
                        snapshot.Id = ID.NewID();
                        snapshot.IsDeleted = false;
                        snapshot.CreateTime = DateTime.Now;
                        snapshot.Name = item.Name;
                        snapshot.Description = item.Description;
                        snapshot.IsEnable = item.IsEnable;
                        snapshot.ChargeConfig = item.ChargeConfig;
                        snapshot.ChargeMode = item.ChargeMode;
                        snapshot.SnapshotCount = item.SnapshotCount;
                        snapshot.EffectiveTime = item.EffectiveTime;
                        snapshot.ExpireTime = DateTime.Now;
                        snapshot.IsSnapshot = true;
                        snapshot.OriginId = item.OriginId;

                        await _repository.InsertAsync(snapshot);

                        item.ChargeMode = model.ChargeMode;
                        item.ChargeConfig = model.ChargeConfig;
                        item.EffectiveTime = DateTime.Now;
                        item.SnapshotCount = item.SnapshotCount + 1;
                    }
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.IsEnable = model.IsEnable;

                    _repository.Update(item);
                    await _repository.SaveAsync();

                    trans.Commit();
                }
            }    
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

        #endregion
    }
}
