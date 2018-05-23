using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.Product.Sku
{
    /// <summary>
    /// 商品Sku 编辑页面
    /// </summary>
    [Auth("mall.product.sku")]
    public class EditModel : BasePage
    {
        private readonly IProductSkuService _service;
        private ICacheService _cacheService;

        public EditModel(IProductSkuService service, ICacheService cacheService)
        {
            this._service = service;
            this._cacheService = cacheService;
        }

        [BindProperty]
        public ProductSkuDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long EditID)
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
            }
            else
            {
                Dto = new ProductSkuDto();
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
            //临时SKU数据，保存到缓存中
            var cacheKey = string.Format(CacheKeyDefinition.ProductSkuTemp, EditID);
            var list = _cacheService.Get<List<ProductSkuDto>>(cacheKey);
            if (list == null)
            {
                list = new List<ProductSkuDto>();
            }
            //TODO
            if (Dto.Id == 0)
            {
                Dto.Id = ID.NewID();
                Dto.CreateTime = DateTime.Now;
                Dto.ModifyStatus = Domain.Enum.EmEntityModifyStatus.Insert;
                list.Add(Dto);
            }
            else
            {
                var index = list.FindIndex(x => x.Id == Dto.Id);
                var savedItem = list[index];
                Dto.ModifyStatus = savedItem.ModifyStatus;
                if (Dto.ModifyStatus != Domain.Enum.EmEntityModifyStatus.Insert)
                {
                    Dto.ModifyStatus = Domain.Enum.EmEntityModifyStatus.Update;
                }
                list[index] = Dto;
            }
            _cacheService.Add(cacheKey, list, new TimeSpan(10, 0, 0));
            return JsonData(true);
        }

        [Auth("edit")]
        public async Task<IActionResult> OnPostDeleteAsync(long id, long EditID)
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
    }
}