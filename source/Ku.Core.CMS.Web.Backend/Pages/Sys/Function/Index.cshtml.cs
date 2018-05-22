using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.Sys.Function
{
    /// <summary>
    /// 功能 列表页面
    /// </summary>
    [Auth("system.function")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IFunctionService _service;

        public IndexModel(IFunctionService service)
        {
            this._service = service;
        }

        public List<FunctionDto> Parents { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long? parentId)
        {
            Parents = new List<FunctionDto>();
            if (parentId.HasValue)
            {
                Parents = await _service.GetParentsAsync(parentId.Value);
            }
            ViewData["ParentId"] = parentId;
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, FunctionSearch where)
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
    }
}
