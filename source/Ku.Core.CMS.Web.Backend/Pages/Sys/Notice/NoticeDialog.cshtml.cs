using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.Sys.Notice
{
    public class NoticeDialogModel : BasePage
    {
        private readonly INoticeService _service;

        public NoticeDialogModel(INoticeService service)
        {
            this._service = service;
        }

        public List<NoticeDto> Dtos { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        public async Task OnGetAsync()
        {
            var data = await _service.GetListAsync(1, 10, new NoticeSearch { IsDeleted = false, IsPublished = true }, new { StickyNum = "desc", PublishedTime = "desc" });
            Dtos = data.items;
        }
    }
}