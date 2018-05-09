using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.AppFeedback
{
    [Auth("datacenter.appfeedback")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IAppFeedbackService _service;
        private readonly IAppService _appService;
        protected readonly ICacheService _cache;

        public IndexModel(IAppFeedbackService service, IAppService appService, ICacheService cache)
        {
            this._service = service;
            this._appService = appService;
            this._cache = cache;
        }

        public List<AppDto> Apps { set; get; }

        [Auth("view")]
        public async Task OnGetAsync()
        {
            Apps = await _appService.GetListAsync(new AppSearch { IsDeleted = false }, null);
        }

        public async Task<IActionResult> OnGetUnsolvedCountAsync()
        {
            var apps = await _appService.GetListAsync(new AppSearch { IsDeleted = false }, null);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var item in apps)
            {
                var UnsolvedCount = _cache.Get<int>(string.Format(CacheKeyDefinition.DataCenter_AppFeedback_Unsolved, item.Id));
                dict.Add(item.Id.ToString(), UnsolvedCount);
            }

            return Json(dict);
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, AppFeedbackSearch where)
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
