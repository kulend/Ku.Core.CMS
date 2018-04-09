//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsQueueService.cs
// 功能描述：短信队列 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public partial class SmsQueueService : BaseService, ISmsQueueService
    {
        protected readonly ISmsQueueRepository _repository;

        #region 构造函数

        public SmsQueueService(ISmsQueueRepository repository)
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
        /// <returns>List<SmsQueueDto></returns>
        public async Task<List<SmsQueueDto>> GetListAsync(SmsQueueSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<SmsQueueDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<SmsQueueDto> items)> GetListAsync(int page, int size, SmsQueueSearch where, string sort)
        {
            var includes = new List<Expression<Func<SmsQueue, object>>>();
            includes.Add(x => x.Sms);
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc", includes.ToArray());
            return (data.count, Mapper.Map<List<SmsQueueDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<SmsQueueDto> GetByIdAsync(long id)
        {
            return Mapper.Map<SmsQueueDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(SmsQueueDto dto)
        {
            SmsQueue model = Mapper.Map<SmsQueue>(dto);
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
                    throw new VinoDataNotFoundException("无法取得短信队列数据！");
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

        #endregion
    }
}
