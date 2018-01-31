using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.IService.System;

namespace Vino.Core.CMS.Web.Admin.Components
{
    [ViewComponent(Name = "AuthCodeJs")]
    public class AuthCodeJsViewComponent : ViewComponent
    {
        private IFunctionService service;

        public AuthCodeJsViewComponent(IFunctionService _service)
        {
            this.service = _service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> codesList = new List<string>();
            var userId = HttpContext.User.GetUserIdOrZero();
            if (userId == 0)
            {
                return View(codesList);
            }
            
            codesList = await service.GetUserAuthCodesAsync(userId, true);
            return View(codesList);
        }
    }
}
