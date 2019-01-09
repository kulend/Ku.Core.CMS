using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Extensions;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Video
{
    /// <summary>
    /// 视频素材 列表页面
    /// </summary>
    [Auth("materialcenter.video")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IVideoService _service;
        private readonly IMaterialCenterConfigService _configService;

        public IndexModel(IVideoService service, IMaterialCenterConfigService configService)
        {
            _service = service;
            _configService = configService;
        }

        [Auth("view")]
        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, VideoSearch where)
        {
            var config = await _configService.GetAsync();

            var data = await _service.GetListAsync(page, rows, where, null);
            if (config.FileSavePath.IsNotNullOrEmpty())
            {
                foreach (var item in data.items)
                {
                    item.Url = config.FileSavePath.Contact(item.FilePath);
                    item.ThumbUrl = config.FileSiteUrl.Contact(item.ThumbPath);
                }
            }
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
