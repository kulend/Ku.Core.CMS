using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Domain.Dto.DataCenter;
using Vino.Core.CMS.Domain.Entity.DataCenter;
using Vino.Core.CMS.IService.DataCenter;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Pages.DataCenter.AppFeedback
{
    [Auth("datacenter.appfeedback")]
    public class DetailModel : BasePage
    {
        private readonly IAppFeedbackService _service;

        public DetailModel(IAppFeedbackService service)
        {
            this._service = service;
        }

        public AppFeedbackDto Dto { set; get; }

        [Auth("detail")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new VinoDataNotFoundException();
            }
        }
    }
}
