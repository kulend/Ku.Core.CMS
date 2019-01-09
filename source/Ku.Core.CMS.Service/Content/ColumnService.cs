//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ColumnService.cs
// 功能描述：栏目 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-23 14:15
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using System.Linq;
using Ku.Core.Infrastructure.Extensions;

namespace Ku.Core.CMS.Service.Content
{
    public partial class ColumnService : BaseService<Column, ColumnDto, ColumnSearch>, IColumnService
    {
        private readonly ICacheProvider _cache;

        #region 构造函数

        public ColumnService(ICacheProvider cache)
        {
            _cache = cache;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(ColumnDto dto)
        {
            Column model = Mapper.Map<Column>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.Tags = "|" + string.Join("|", model.Tags.SplitRemoveEmpty(',')) + "|";
                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        model.Name,
                        model.Title,
                        model.OrderIndex,
                        model.Tag,
                        Tags = "|" + string.Join("|", model.Tags.SplitRemoveEmpty(',')) + "|"
                    };
                    await dapper.UpdateAsync<Column>(item, new { model.Id });
                }
            }

            //清除缓存
            await _cache.RemoveAsync(CacheKeyDefinition.ContentColumnList);
            await _cache.RemoveAsync(CacheKeyDefinition.PcSiteColumn);
        }

        #region 其他方法

        public async Task<List<ColumnDto>> GetParentsAsync(long parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = new List<Column>();
                async Task GetWhithParentAsync(long pid)
                {
                    var model = await dapper.QueryOneAsync<Column>(new { Id = pid });
                    if (model != null)
                    {
                        if (model.ParentId.HasValue)
                        {
                            await GetWhithParentAsync(model.ParentId.Value);
                        }
                        list.Add(model);
                    }
                }

                await GetWhithParentAsync(parentId);
                return Mapper.Map<List<ColumnDto>>(list);
            }
        }

        /// <summary>
        /// 从缓存获取栏目列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ColumnDto>> GetListFromCacheAsync()
        {
            if (await _cache.ExistsAsync(CacheKeyDefinition.ContentColumnList))
            {
                return await _cache.GetAsync<List<ColumnDto>>(CacheKeyDefinition.ContentColumnList);
            }

            using (var dapper = DapperFactory.Create())
            {
                var list = (await dapper.QueryListAsync<Column>(where: null, order: null)).ToList();
                foreach (var item in list)
                {
                    item.Subs = new List<Column>();
                }
                foreach (var item in list)
                {
                    if (item.ParentId.HasValue)
                    {
                        var p = list.FirstOrDefault(x => x.Id == item.ParentId.Value);
                        if (p != null)
                        {
                            p.Subs.Add(item);
                        }
                    }
                }

                var data = Mapper.Map<List<ColumnDto>>(list);
                await _cache.SetAsync(CacheKeyDefinition.ContentColumnList, data);

                return data;
            }
        }

        #endregion
    }
}
