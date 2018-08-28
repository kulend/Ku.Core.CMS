using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.PageViewRecord
{
    /// <summary>
    /// 页面浏览记录 列表页面
    /// </summary>
    [Auth("datacenter.pageviewrecord")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IPageViewRecordService _service;

        public IndexModel(IPageViewRecordService service)
        {
            this._service = service;
        }

        [Auth("view")]
        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, PageViewRecordSearch where)
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
