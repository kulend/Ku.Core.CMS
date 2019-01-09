using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.ArticleUserEvent
{
    /// <summary>
    /// 文章用户事件 详情页面
    /// </summary>
    [Auth("content.articleuserevent")]
    public class DetailModel : BasePage
    {
        private readonly IArticleUserEventService _service;

        public DetailModel(IArticleUserEventService service)
        {
            this._service = service;
        }

        public ArticleUserEventDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }
    }
}
