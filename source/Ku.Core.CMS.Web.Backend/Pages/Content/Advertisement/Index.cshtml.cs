using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Advertisement
{
    /// <summary>
    /// 广告 列表页面
    /// </summary>
    [Auth("content.advertisement")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IAdvertisementService _service;
        private readonly IAdvertisementBoardService _boardService;

        public IndexModel(IAdvertisementService service, IAdvertisementBoardService boardService)
        {
            _service = service;
            _boardService = boardService;
        }

        /// <summary>
        /// 广告位列表
        /// </summary>
        public IEnumerable<AdvertisementBoardDto> Boards { get; set; }

        [Auth("view")]
        public async Task OnGetAsync(long? id)
        {
            //广告位列表取得
            Boards = await _boardService.GetListAsync(new AdvertisementBoardSearch { IsDeleted = false }, null);
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, AdvertisementSearch where)
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
