//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductCategoryService.cs
// 功能描述：商品类目 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-26 11:32
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Mall;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.Mall;
using Vino.Core.CMS.Domain.Entity.Mall;
using Vino.Core.CMS.IService.Mall;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Mall
{
    public partial class ProductCategoryService : BaseService, IProductCategoryService
    {
        protected readonly IProductCategoryRepository _repository;

        #region 构造函数

        public ProductCategoryService(IProductCategoryRepository repository)
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
        /// <returns>List<ProductCategoryDto></returns>
        public async Task<List<ProductCategoryDto>> GetListAsync(ProductCategorySearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<ProductCategoryDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<ProductCategoryDto> items)> GetListAsync(int page, int size, ProductCategorySearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<ProductCategoryDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<ProductCategoryDto> GetByIdAsync(long id)
        {
            return Mapper.Map<ProductCategoryDto>(await this._repository.GetByIdAsync(id));
        }

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

                if (model.ParentId.HasValue)
                {
                    var parent = await _repository.GetByIdAsync(model.ParentId.Value);
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

                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得商品类目数据！");
                }

                //这里进行赋值
                item.Name = model.Name;

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

        public async Task<List<ProductCategoryDto>> GetWithParentsAsync(long id)
        {
            var list = new List<ProductCategory>();
            async Task GetModel(long pid)
            {
                var model = _repository.FirstOrDefault(x => x.Id == pid);
                if (model != null)
                {
                    if (model.ParentId.HasValue)
                    {
                        await GetModel(model.ParentId.Value);
                    }
                    list.Add(model);
                }
            }
            await GetModel(id);
            return Mapper.Map<List<ProductCategoryDto>>(list);
        }

        /// <summary>
        /// 取得json数据
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetJsonDataAsync()
        {
            var data = await _repository.QueryAsync(x => !x.IsDeleted, null);
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


        #endregion
    }
}
