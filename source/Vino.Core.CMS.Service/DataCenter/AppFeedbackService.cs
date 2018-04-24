//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppFeedbackService.cs
// 功能描述：应用反馈 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Repository.DataCenter;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.DataCenter;
using Vino.Core.CMS.Domain.Entity.DataCenter;
using Vino.Core.CMS.IService.DataCenter;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.DataCenter
{
    public partial class AppFeedbackService : BaseService, IAppFeedbackService
    {
        protected readonly IAppFeedbackRepository _repository;
        protected readonly ICacheService _cache;

        #region 构造函数

        public AppFeedbackService(IAppFeedbackRepository repository, ICacheService cache)
        {
            this._repository = repository;
            this._cache = cache;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<AppFeedbackDto></returns>
        public async Task<List<AppFeedbackDto>> GetListAsync(AppFeedbackSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<AppFeedbackDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<AppFeedbackDto> items)> GetListAsync(int page, int size, AppFeedbackSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<AppFeedbackDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<AppFeedbackDto> GetByIdAsync(long id)
        {
            return Mapper.Map<AppFeedbackDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AppFeedbackDto dto)
        {
            AppFeedback model = Mapper.Map<AppFeedback>(dto);
            model.Id = ID.NewID();
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;
            model.Resolved = false;

            await _repository.InsertAsync(model);
            await _repository.SaveAsync();

            var UnsolvedCount = _cache.Get<int>(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, model.AppId));
            UnsolvedCount++;
            _cache.Add(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, model.AppId), UnsolvedCount);
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
        /// 处理反馈
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task ResolveAsync(AppFeedbackDto dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            if (item == null)
            {
                throw new VinoDataNotFoundException();
            }

            var beforeStatus = item.Resolved;

            item.Resolved = dto.Resolved;
            if (item.Resolved)
            {
                item.ResolveTime = DateTime.Now;
            }
            item.AdminRemark = dto.AdminRemark;

            _repository.Update(item);

            await _repository.SaveAsync();

            var UnsolvedCount = _cache.Get<int>(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId));
            if (item.Resolved && !beforeStatus)
            {
                UnsolvedCount--;
                if (UnsolvedCount < 0) UnsolvedCount = 0;
                _cache.Add(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId), UnsolvedCount);
            } else if (!item.Resolved && beforeStatus)
            {
                UnsolvedCount++;
                _cache.Add(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId), UnsolvedCount);
            }
        }

        #endregion
    }
}
