using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.DeliveryTemplet
{
    /// <summary>
    /// 配送模板 列表页面
    /// </summary>
    [Auth("mall.deliverytemplet")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IDeliveryTempletService _service;

        public IndexModel(IDeliveryTempletService service)
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
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, DeliveryTempletSearch where)
        {
            if (where == null)
            {
                where = new DeliveryTempletSearch();
            }
            if (!where.IsSnapshot.HasValue)
            {
                where.IsSnapshot = false;
            }
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
