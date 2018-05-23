using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Newtonsoft.Json;

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.Product
{
    /// <summary>
    /// 商品 编辑页面
    /// </summary>
    [Auth("mall.product")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class EditModel : BasePage
    {
        private readonly IProductService _service;
        private readonly IProductSkuService _skuService;
        private ICacheService _cacheService;
        private readonly IProductCategoryService _productCategoryService;

        public EditModel(IProductService service, IProductSkuService skuService, ICacheService cacheService
            , IProductCategoryService productCategoryService)
        {
            this._service = service;
            this._skuService = skuService;
            this._cacheService = cacheService;
            this._productCategoryService = productCategoryService;
        }

        [BindProperty]
        public ProductDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
        {
            //生成编辑ID，sku变动时根据此ID来操作缓存数据
            var EditID = ID.NewID();
            ViewData["EditID"] = EditID;

            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new VinoDataNotFoundException();
                }

                //取得所有sku并保存到缓存
                ProductSkuSearch where = new ProductSkuSearch();
                where.ProductId = Dto.Id;
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
                if (Dto.CategoryId.HasValue)
                {
                    (await _productCategoryService.GetWithParentsAsync(Dto.CategoryId.Value)).ForEach(x => dict.Add(x.Level, x.Id));
                }

                ViewData["Category"] = JsonConvert.SerializeObject(dict);
                //ViewBag.Category = JsonConvert.SerializeObject(dict);

                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new ProductDto();
                ViewData["Category"] = "";
                ViewData["Mode"] = "Add";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync(long EditID)
        {
            if (!ModelState.IsValid)
            {
                throw new VinoArgNullException();
            }

            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);

            await _service.SaveAsync(Dto, list);
            return Json(true);
        }

        /// <summary>
        /// 取得Sku列表数据
        /// </summary>
        [Auth("edit")]
        public IActionResult OnPostSkuList(int page, int rows, long EditID)
        {
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
            if (list == null)
            {
                list = new List<ProductSkuDto>();
            }
            return PagerData(list.Where(x => x.ModifyStatus != Domain.Enum.EmEntityModifyStatus.Delete), 1, 99, list.Count);
        }
    }
}
