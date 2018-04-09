//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsService.cs
// 功能描述：短信 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.IService.System;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.System
{
    public partial class SmsService : BaseService, ISmsService
    {
        protected readonly ISmsRepository _repository;
        protected readonly ISmsQueueRepository _queueRepository;
        protected readonly ISmsTempletRepository _templetRepository;

        #region 构造函数

        public SmsService(ISmsRepository repository, 
            ISmsQueueRepository queueRepository, ISmsTempletRepository templetRepository)
        {
            this._repository = repository;
            this._queueRepository = queueRepository;
            this._templetRepository = templetRepository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<SmsDto></returns>
        public async Task<List<SmsDto>> GetListAsync(SmsSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<SmsDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<SmsDto> items)> GetListAsync(int page, int size, SmsSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<SmsDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<SmsDto> GetByIdAsync(long id)
        {
            return Mapper.Map<SmsDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(SmsDto dto)
        {
            Sms model = Mapper.Map<Sms>(dto);
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
                    throw new VinoDataNotFoundException("无法取得短信数据！");
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
        /// 新增
        /// </summary>
        public async Task AddAsync(SmsDto dto)
        {
            Sms model = Mapper.Map<Sms>(dto);

            if (string.IsNullOrEmpty(model.Mobile))
            {
                throw new VinoArgNullException("未设置手机号！");
            }

            var templet = await _templetRepository.GetByIdAsync(model.SmsTempletId);
            if (templet == null || templet.IsDeleted)
            {
                throw new VinoDataNotFoundException("无法取得短信模板数据！");
            }
            if (!templet.IsEnable)
            {
                throw new VinoDataNotFoundException("短信模板已禁用！");
            }

            var content = templet.Templet;
            foreach (var item in dto.Data)
            {
                content = content.Replace("${" + item.Key + "}", item.Value);
            }
            model.Content = content + $"【{templet.Signature}】";

            model.Id = ID.NewID();
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;

            //创建队列
            var queue = new SmsQueue {
                Id = ID.NewID(),
                CreateTime = DateTime.Now,
                IsDeleted = false,
                Status = Domain.Enum.System.EmSmsQueueStatus.WaitSend,
                SmsId = model.Id,
                ExpireTime = templet.ExpireMinute == 0 ? DateTime.Now.AddDays(1) : DateTime.Now.AddMinutes(templet.ExpireMinute)
            };

            using (var trans = await _repository.BeginTransactionAsync())
            {
                await _repository.InsertAsync(model);
                await _queueRepository.InsertAsync(queue);
                await _repository.SaveAsync();

                trans.Commit();
            }
        }


        #endregion
    }
}
