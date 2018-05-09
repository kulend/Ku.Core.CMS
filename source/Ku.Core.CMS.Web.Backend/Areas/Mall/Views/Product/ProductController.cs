//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ProductController.cs
// 功能描述：商品 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-24 10:50
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Web.Security;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.Infrastructure.IdGenerator;
using Newtonsoft.Json;

namespace Ku.Core.CMS.Web.Backend.Areas.Mall.Views.Product
{
    [Area("Mall")]
    [Auth("mall.product")]
    public class ProductController : BackendController
    {
        private readonly IProductService _service;
        private readonly IProductSkuService _skuService;
        private ICacheService _cacheService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductController(IProductService service, IProductSkuService skuService, ICacheService cacheService
            , IProductCategoryService productCategoryService
            )
        {
            this._service = service;
            this._skuService = skuService;
            this._cacheService = cacheService;
            this._productCategoryService = productCategoryService;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, ProductSearch where)
        {
            if (where == null)
            {
                where = new ProductSearch { IsDeleted = false};
            }
            if (!where.IsSnapshot.HasValue)
            {
                where.IsSnapshot = false;
            }
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            //生成编辑ID，sku变动时根据此ID来操作缓存数据
            var EditID = ID.NewID();
            ViewData["EditID"] = EditID;

            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                //取得所有sku并保存到缓存
                ProductSkuSearch where = new ProductSkuSearch();
                where.ProductId = model.Id;
                var skus = await _skuService.GetListAsync(where, null);
                if (skus == null)
                {
                    skus = new List<ProductSkuDto>();
                }
                foreach (var sku in skus)
                {
                    sku.ModifyStatus = Domain.Enum.EmEntityModifyStatus.UnChange;
                }
                var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
                _cacheService.Add(cacheKey, skus, new TimeSpan(10, 0, 0));

                //取得商品类目数据
                var dict = new Dictionary<int, long>();
                if (model.CategoryId.HasValue)
                {
                    (await _productCategoryService.GetWithParentsAsync(model.CategoryId.Value)).ForEach(x=> dict.Add(x.Level, x.Id));
                }

                ViewBag.Category = JsonConvert.SerializeObject(dict);

                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                ProductDto dto = new ProductDto();

                ViewBag.Category = "";

                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        #region SKU编辑

        [Auth("edit")]
        public async Task<IActionResult> GetSkuList(int page, int rows, long EditID)
        {
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
            if (list == null)
            {
                list = new List<ProductSkuDto>();
            }
            return PagerData(list.Where(x=>x.ModifyStatus != Domain.Enum.EmEntityModifyStatus.Delete), 1, 99, list.Count);
        }

        [Auth("edit")]
        public async Task<IActionResult> SkuEdit(long? id, long EditID)
        {
            ViewData["EditID"] = EditID;

            if (id.HasValue)
            {
                //编辑
                //从缓存获取
                var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
                var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
                if (list == null)
                {
                    list = new List<ProductSkuDto>();
                }
                var model = list.SingleOrDefault(x => x.Id == id);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                ProductSkuDto dto = new ProductSkuDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存SKU
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> SkuSave(ProductSkuDto model, long EditID)
        {
            //临时SKU数据，保存到缓存中
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
            if (list == null)
            {
                list = new List<ProductSkuDto>();
            }
            //TODO
            if (model.Id == 0)
            {
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.ModifyStatus = Domain.Enum.EmEntityModifyStatus.Insert;
                list.Add(model);
            }
            else
            {
                var index = list.FindIndex(x => x.Id == model.Id);
                var savedItem = list[index];
                model.ModifyStatus = savedItem.ModifyStatus;
                if (model.ModifyStatus != Domain.Enum.EmEntityModifyStatus.Insert)
                {
                    model.ModifyStatus = Domain.Enum.EmEntityModifyStatus.Update;
                }
                list[index] = model;
            }
            _cacheService.Add(cacheKey, list, new TimeSpan(10, 0, 0));
            return JsonData(true);
        }

        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> SkuDelete(long id, long EditID)
        {
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
            if (list == null)
            {
                list = new List<ProductSkuDto>();
            }
            var item = list.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                if (item.ModifyStatus == Domain.Enum.EmEntityModifyStatus.Insert)
                {
                    list.Remove(item);
                }
                else
                {
                    item.ModifyStatus = Domain.Enum.EmEntityModifyStatus.Delete;
                }
            }
            _cacheService.Add(cacheKey, list, new TimeSpan(10, 0, 0));
            return JsonData(true);
        }

        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(ProductDto model, long EditID)
        {
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);

            await _service.SaveAsync(model, list);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(params long[] id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("restore")]
        public async Task<IActionResult> Restore(params long[] id)
        {
            await _service.RestoreAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 商品属性选择
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AttrSelect()
        {
            return View();
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        [Auth("detail")]
        public async Task<IActionResult> Detail(long id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }

            return View(model);
        }

        [Auth("detail")]
        public async Task<IActionResult> GetProductSkuList(long ProductId)
        {
            ProductSkuSearch where = new ProductSkuSearch();
            where.ProductId = ProductId;
            var list = await _skuService.GetListAsync(where, "OrderIndex asc");

            return PagerData(list, 1, 999, list.Count);
        }

        /// <summary>
        /// 商品SKU详情
        /// </summary>
        [Auth("detail")]
        public async Task<IActionResult> SkuDetail(long id)
        {
            var model = await _skuService.GetByIdAsync(id);
            if (model == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }

            return View(model);
        }
    }
}
