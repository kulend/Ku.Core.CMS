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

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.Product
{
    /// <summary>
    /// 商品 详情页面
    /// </summary>
    [Auth("mall.product")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class DetailModel : BasePage
    {
        private readonly IProductService _service;
        private readonly IProductSkuService _skuService;

        public DetailModel(IProductService service, IProductSkuService skuService)
        {
            this._service = service;
            _skuService = skuService;
        }

        public ProductDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostSkuListAsync(long ProductId)
        {
            ProductSkuSearch where = new ProductSkuSearch();
            where.ProductId = ProductId;
            var list = await _skuService.GetListAsync(where, "OrderIndex asc");

            return PagerData(list, 1, 999, list.Count);
        }
    }
}
