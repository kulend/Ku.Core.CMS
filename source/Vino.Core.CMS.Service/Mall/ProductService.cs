//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductService.cs
// 功能描述：商品 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ProductService : BaseService, IProductService
    {
        protected readonly IProductRepository _repository;
        protected readonly IProductSkuRepository _skuRepository;

        #region 构造函数

        public ProductService(IProductRepository repository, IProductSkuRepository skuRepository)
        {
            this._repository = repository;
            this._skuRepository = skuRepository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<ProductDto></returns>
        public async Task<List<ProductDto>> GetListAsync(ProductSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<ProductDto>>(data, opt => {
                opt.Items.Add("JsonDeserializeIgnore", true);
            });
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<ProductDto> items)> GetListAsync(int page, int size, ProductSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<ProductDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<ProductDto> GetByIdAsync(long id)
        {
            return Mapper.Map<ProductDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(ProductDto dto, List<ProductSkuDto> skus)
        {
            Product model = Mapper.Map<Product>(dto);
            List<ProductSku> skuList = Mapper.Map<List<ProductSku>>(skus);
            if (skuList == null || skuList.Count == 0)
            {
                throw new VinoDataNotFoundException("至少添加一项商品SKU！");
            }
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.Sales = 0;
                model.Visits = 0;
                model.IsDeleted = false;
                model.IsSnapshot = false;
                model.SnapshotCount = 0;
                model.OriginId = null;
                model.EffectiveTime = DateTime.Now;

                foreach (var item in skuList)
                {
                    item.ProductId = model.Id;
                    item.CreateTime = DateTime.Now;
                    item.IsDeleted = false;
                    item.Sales = 0;
                }
                model.Stock = skuList.Sum(x => x.Stock);
                var maxPrice = skuList.Max(x => x.Price);
                var minPrice = skuList.Min(x => x.Price);
                if (maxPrice == minPrice)
                {
                    model.PriceRange = maxPrice + "";
                }
                else
                {
                    model.PriceRange = minPrice + "~" + maxPrice;
                }

                using (var trans = await _repository.BeginTransactionAsync())
                {
                    await _repository.InsertAsync(model);
                    await _skuRepository.InsertRangeAsync(skuList);
                    await _repository.SaveAsync();
                    trans.Commit();
                }
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null || item.IsDeleted)
                {
                    throw new VinoDataNotFoundException("无法取得商品数据！");
                }
                if (item.IsSnapshot)
                {
                    throw new VinoDataNotFoundException("快照数据无法修改！");
                }

                using (var trans = await _repository.BeginTransactionAsync())
                {
                    //生成快照
                    Product snapshot = item.Copy();
                    snapshot.Id = ID.NewID();
                    snapshot.IsDeleted = false;
                    snapshot.CreateTime = DateTime.Now;
                    snapshot.ExpireTime = DateTime.Now;
                    snapshot.IsSnapshot = true;
                    snapshot.OriginId = item.OriginId;
                    await _repository.InsertAsync(snapshot);

                    //var search = new ProductSkuSearch { ProductId = item.Id };
                    //包含已删除数据
                    var skuItems = await _skuRepository.QueryAsync((x=>x.ProductId == item.Id), null, null);
                    var snapshotSkus = new List<ProductSku>();
                    foreach (var sku in skuItems)
                    {
                        ProductSku snapshotSku = sku.Copy();
                        snapshotSku.Id = ID.NewID();
                        snapshotSku.ProductId = snapshot.Id;
                        snapshotSkus.Add(snapshotSku);
                    }
                    await _skuRepository.InsertRangeAsync(snapshotSkus);
                    //end

                    model.Stock = skuList.Sum(x => x.Stock);
                    var maxPrice = skuList.Max(x => x.Price);
                    var minPrice = skuList.Min(x => x.Price);
                    if (maxPrice == minPrice)
                    {
                        model.PriceRange = maxPrice + "";
                    }
                    else
                    {
                        model.PriceRange = minPrice + "~" + maxPrice;
                    }

                    item.Status = model.Status;
                    item.Name = model.Name;
                    item.Title = model.Title;
                    item.Intro = model.Intro;
                    item.ImageData = model.ImageData;
                    item.ContentType = model.ContentType;
                    item.Content = model.Content;
                    item.PriceRange = model.PriceRange;
                    item.Stock = model.Stock;
                    item.OrderIndex = model.OrderIndex;
                    item.Properties = model.Properties;

                    item.SnapshotCount = item.SnapshotCount + 1;
                    item.EffectiveTime = DateTime.Now;

                    //var search = new ProductSkuSearch { ProductId = item.Id };
                    //var skuItems = await _skuRepository.QueryAsync(search.GetExpression(), null);

                    //新增
                    var newSkus = skuList.Where(x => x.ModifyStatus == Domain.Enum.EmEntityModifyStatus.Insert);
                    foreach (var sku in newSkus)
                    {
                        sku.ProductId = item.Id;
                        sku.CreateTime = DateTime.Now;
                        sku.IsDeleted = false;
                        sku.Sales = 0;
                    }
                    await _skuRepository.InsertRangeAsync(newSkus.ToList());

                    //删除
                    foreach (var sku in skuList.Where(x => x.ModifyStatus == Domain.Enum.EmEntityModifyStatus.Delete))
                    {
                        await _skuRepository.DeleteAsync(sku.Id);
                    }
                    //更新
                    foreach (var sku in skuList.Where(x => x.ModifyStatus == Domain.Enum.EmEntityModifyStatus.Update))
                    {
                        var skuItem = _skuRepository.GetById(sku.Id);
                        if (skuItem == null || skuItem.IsDeleted)
                        {
                            trans.Rollback();
                            throw new VinoDataNotFoundException("SKU数据无法取得！");
                        }
                        skuItem.Name = sku.Name;
                        skuItem.Title = sku.Title;
                        skuItem.CoverImage = sku.CoverImage;
                        skuItem.Price = sku.Price;
                        skuItem.MarketPrice = sku.MarketPrice;
                        skuItem.Stock = sku.Stock;
                        skuItem.OrderIndex = sku.OrderIndex;
                        _skuRepository.Update(skuItem);
                    }

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
