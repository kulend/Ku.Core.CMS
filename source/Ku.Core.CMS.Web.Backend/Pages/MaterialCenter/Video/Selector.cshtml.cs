using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Video
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SelectorModel : BasePage
    {
        private readonly IVideoService _service;
        private readonly IMaterialCenterConfigService _configService;

        public SelectorModel(IVideoService service, IMaterialCenterConfigService configService)
        {
            _service = service;
            _configService = configService;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        //[Auth("view")]
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
    }
}