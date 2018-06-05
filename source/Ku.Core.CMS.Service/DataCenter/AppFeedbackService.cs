//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AppFeedbackService.cs
// 功能描述：应用反馈 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.DataCenter
{
    public partial class AppFeedbackService : BaseService<AppFeedback, AppFeedbackDto, AppFeedbackSearch>, IAppFeedbackService
    {
        protected readonly ICacheProvider _cache;

        #region 构造函数

        public AppFeedbackService(ICacheProvider cache)
        {
            _cache = cache;
        }

        #endregion

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
            using (var dapper = DapperFactory.Create())
            {
                await dapper.InsertAsync(model);
            }

            var UnsolvedCount = await _cache.GetAsync<int>(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, model.AppId));
            UnsolvedCount++;
            await _cache.SetAsync(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, model.AppId), UnsolvedCount);
        }

        /// <summary>
        /// 处理反馈
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task ResolveAsync(AppFeedbackDto dto)
        {
            using (var dapper = DapperFactory.Create())
            {
                var item = await dapper.QueryOneAsync<AppFeedback>(new { Id = dto.Id });
                if (item == null)
                {
                    throw new KuDataNotFoundException();
                }

                dynamic data = new ExpandoObject();
                data.Resolved = dto.Resolved;
                data.AdminRemark = dto.AdminRemark;
                if (dto.Resolved)
                {
                    data.ResolveTime = DateTime.Now;
                }

                await dapper.UpdateAsync<AppFeedback>(data, new { Id = dto.Id });

                var UnsolvedCount = _cache.Get<int>(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId));
                if (dto.Resolved && !item.Resolved)
                {
                    UnsolvedCount--;
                    if (UnsolvedCount < 0) UnsolvedCount = 0;
                    await _cache.SetAsync(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId), UnsolvedCount);
                }
                else if (!dto.Resolved && item.Resolved)
                {
                    UnsolvedCount++;
                    await _cache.SetAsync(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.AppId), UnsolvedCount);
                }
            }
        }
    }
}
