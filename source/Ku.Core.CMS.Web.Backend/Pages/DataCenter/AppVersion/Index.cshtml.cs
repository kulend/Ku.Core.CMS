using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.AppVersion
{
    /// <summary>
    /// 应用版本 列表页面
    /// </summary>
    [Auth("datacenter.appversion")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IAppVersionService _service;
        private readonly IAppService _appService;

        public IndexModel(IAppVersionService service, IAppService appService)
        {
            this._service = service;
            this._appService = appService;
        }

        public List<AppDto> Apps { set; get; }

        [Auth("view")]
        public async Task OnGetAsync()
        {
            Apps = await _appService.GetListAsync(new AppSearch { IsDeleted = false }, null);
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, AppVersionSearch where)
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
