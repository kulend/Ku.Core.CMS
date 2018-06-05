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

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.Product.Category
{
    /// <summary>
    /// 商品类目 编辑页面
    /// </summary>
    [Auth("mall.productcategory")]
    public class EditModel : BasePage
    {
        private readonly IProductCategoryService _service;

        public EditModel(IProductCategoryService service)
        {
            this._service = service;
        }

        [BindProperty]
        public ProductCategoryDto Dto { set; get; }

        public List<ProductCategoryDto> Parents { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? pid)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                if (Dto.ParentId.HasValue)
                {
                    Parents = await _service.GetWithParentsAsync(Dto.ParentId.Value);
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new ProductCategoryDto();
                if (pid.HasValue)
                {
                    Dto.ParentId = pid.Value;
                    Parents = await _service.GetWithParentsAsync(pid.Value);
                }
                else
                {
                    Dto.ParentId = null;
                }
                ViewData["Mode"] = "Add";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new KuArgNullException();
            }

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
