using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vino.Core.CMS.Domain.Dto.Content;
using Vino.Core.CMS.Domain.Entity.Content;
using Vino.Core.CMS.IService.Content;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;

namespace Vino.Core.CMS.Web.Backend.Pages.Content.Column
{
    [Auth("content.column")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IColumnService _service;

        public IndexModel(IColumnService service)
        {
            this._service = service;
        }

        public List<ColumnDto> Parents { set; get; }

        public async Task OnGetAsync(long? parentId)
        {
            Parents = new List<ColumnDto>();
            if (parentId.HasValue)
            {
                Parents = await _service.GetParentsAsync(parentId.Value);
            }
            ViewData["ParentId"] = parentId;
        }

        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, ColumnSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [Auth("delete")]
        public async Task<IActionResult> OnPostDeleteAsync(params long[] id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [Auth("restore")]
        public async Task<IActionResult> OnPostRestoreAsync(params long[] id)
        {
            await _service.RestoreAsync(id);
            return JsonData(true);
        }
    }
}