//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ProductCategoryService.cs
// 功能描述：商品类目 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-26 11:32
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Mall
{
    public partial class ProductCategoryService : BaseService<ProductCategory, ProductCategoryDto, ProductCategorySearch>, IProductCategoryService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(ProductCategoryDto dto)
        {
            ProductCategory model = Mapper.Map<ProductCategory>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;

                using (var dapper = DapperFactory.Create())
                {
                    if (model.ParentId.HasValue)
                    {
                        var parent = await dapper.QueryOneAsync<ProductCategory>(new { Id = model.ParentId.Value });
                        if (parent == null)
                        {
                            throw new VinoDataNotFoundException("无法取得商品类目数据！");
                        }
                        model.Level = parent.Level + 1;
                    }
                    else
                    {
                        model.Level = 1;
                    }

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
                        model.Name
                    };
                    await dapper.UpdateAsync<ProductCategory>(item, new { model.Id });
                }
            }
        }

        public async Task<List<ProductCategoryDto>> GetWithParentsAsync(long id)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = new List<ProductCategory>();
                async Task GetWhithParentAsync(long pid)
                {
                    var model = await dapper.QueryOneAsync<ProductCategory>(new { Id = pid });
                    if (model != null)
                    {
                        if (model.ParentId.HasValue)
                        {
                            await GetWhithParentAsync(model.ParentId.Value);
                        }
                        list.Add(model);
                    }
                }

                await GetWhithParentAsync(id);
                return Mapper.Map<List<ProductCategoryDto>>(list);
            }
        }

        /// <summary>
        /// 取得json数据
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetJsonDataAsync()
        {
            IEnumerable<ProductCategory> data = new List<ProductCategory>();
            using (var dapper = DapperFactory.Create())
            {
                data = await dapper.QueryListAsync<ProductCategory>(new { IsDeleted = false});
            }

            var maxLevel = data.Max(x=>x.Level);

            var dict = new Dictionary<int, object>();
            for (int i = 1; i <= maxLevel; i++)
            {
                if (!data.Any(x => x.Level == i))
                {
                    break;
                }

                var dts = new Dictionary<long, object>();
                if (i == 1)
                {
                    foreach (var item in data.Where(x => x.Level == i))
                    {
                        dts.Add(item.Id, item.Name);
                    }
                }
                else
                {
                    foreach (var parentId in data.Where(x => x.Level == i).Select(x => x.ParentId).Distinct())
                    {
                        var dt = new Dictionary<long, string>();
                        foreach (var item in data.Where(x => x.ParentId == parentId))
                        {
                            dt.Add(item.Id, item.Name);
                        }
                        dts.Add(parentId.Value, dt);
                    }
                }
                dict.Add(i, dts);
            }

            return dict;
        }
    }
}
