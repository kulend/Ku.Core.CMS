using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Domain.Dto.Mall;
using Vino.Core.CMS.Domain.Entity.Mall;
using Vino.Core.CMS.IService.Mall;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Pages.Mall.Product.Category
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
                    throw new VinoDataNotFoundException();
                }
                if (Dto.ParentId.HasValue)
                {
                    Parents = await _service.GetParentsAsync(Dto.ParentId.Value);
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new ProductCategoryDto();
                if (pid.HasValue)
                {
                    Dto.ParentId = pid.Value;
                    Parents = await _service.GetParentsAsync(pid.Value);
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
                throw new VinoArgNullException();
            }

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
