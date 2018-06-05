using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.AppFeedback
{
    [Auth("datacenter.app.feedback")]
    public class DetailModel : BasePage
    {
        private readonly IAppFeedbackService _service;

        public DetailModel(IAppFeedbackService service)
        {
            this._service = service;
        }

        [BindProperty]
        public AppFeedbackDto Dto { set; get; }

        [Auth("detail")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }

        [Auth("resolve")]
        public async Task<IActionResult> OnPostAsync()
        {
            await _service.ResolveAsync(Dto);
            return Json(true);
        }
    }
}
