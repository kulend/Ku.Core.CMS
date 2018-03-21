//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：PaymentService.cs
// 功能描述：支付方式 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-08 13:31
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Mall;
using Vino.Core.CMS.Domain.Dto.Mall;
using Vino.Core.CMS.Domain.Entity.Mall;
using Vino.Core.CMS.IService.Mall;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Mall
{
    public partial class PaymentService : BaseService, IPaymentService
    {
        protected readonly IPaymentRepository _repository;

        #region 构造函数

        public PaymentService(IPaymentRepository repository)
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
        /// <returns>List<PaymentDto></returns>
        public async Task<List<PaymentDto>> GetListAsync(PaymentSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<PaymentDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<PaymentDto> items)> GetListAsync(int page, int size, PaymentSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<PaymentDto>>(data.items, opt => {
                opt.Items.Add("JsonDeserializeIgnore", true);
            }));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<PaymentDto> GetByIdAsync(long id)
        {
            return Mapper.Map<PaymentDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(PaymentDto dto)
        {
            Payment model = Mapper.Map<Payment>(dto);
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
                    throw new VinoDataNotFoundException("无法取得支付方式数据！");
                }
                using (var trans = await _repository.BeginTransactionAsync())
                {
                    //生成快照
                    if (item.PaymentMode != model.PaymentMode
                        || !item.PaymentConfig.Equals(model.PaymentConfig))
                    {
                        Payment snapshot = new Payment();
                        snapshot.Id = ID.NewID();
                        snapshot.IsDeleted = false;
                        snapshot.CreateTime = DateTime.Now;
                        snapshot.Name = item.Name;
                        snapshot.Description = item.Description;
                        snapshot.IsEnable = item.IsEnable;
                        snapshot.IsMobile = item.IsMobile;
                        snapshot.PaymentConfig = item.PaymentConfig;
                        snapshot.PaymentMode = item.PaymentMode;
                        snapshot.SnapshotCount = item.SnapshotCount;
                        snapshot.EffectiveTime = item.EffectiveTime;
                        snapshot.ExpireTime = DateTime.Now;
                        snapshot.IsSnapshot = true;
                        snapshot.OriginId = item.OriginId;

                        await _repository.InsertAsync(snapshot);

                        item.PaymentMode = model.PaymentMode;
                        item.PaymentConfig = model.PaymentConfig;
                        item.EffectiveTime = DateTime.Now;
                        item.SnapshotCount = item.SnapshotCount + 1;
                    }
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.IsEnable = model.IsEnable;
                    item.IsMobile = model.IsMobile;

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
