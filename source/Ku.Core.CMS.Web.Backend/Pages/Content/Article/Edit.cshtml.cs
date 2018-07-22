using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Article
{
    /// <summary>
    /// 文章 编辑页面
    /// </summary>
    [Auth("content.article")]
    public class EditModel : BasePage
    {
        private readonly IArticleService _service;
        private readonly IColumnService _service2;

        public EditModel(IArticleService service, IColumnService service2)
        {
            _service = service;
            _service2 = service2;
        }

        [BindProperty]
        public ArticleDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? ColumnId)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                Dto.Column = await _service2.GetByIdAsync(Dto.ColumnId);
                if (Dto.Column == null)
                {
                    throw new KuDataNotFoundException("数据出错!");
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new ArticleDto();
                if (ColumnId.HasValue)
                {
                    Dto.ColumnId = ColumnId.Value;
                    Dto.Column = await _service2.GetByIdAsync(ColumnId.Value);
                    if (Dto.Column == null)
                    {
                        throw new KuDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    throw new KuDataNotFoundException("参数出错!");
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
