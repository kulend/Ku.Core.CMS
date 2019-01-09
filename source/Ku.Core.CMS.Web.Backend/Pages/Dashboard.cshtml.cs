using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class DashboardModel : BasePage
    {
        private readonly ICacheProvider _cache;

        public DashboardModel(ICacheProvider cache)
        {
            _cache = cache;
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        [Auth("clearcache")]
        public async Task<IActionResult> OnPostClearCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
            return JsonData(true);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        [Auth("clearcache")]
        public async Task<IActionResult> OnPostClearCacheByPrefixAsync(string prefix)
        {
            await _cache.RemoveByPrefixAsync(prefix);
            return JsonData(true);
        }
    }
}