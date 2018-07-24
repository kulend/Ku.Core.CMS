using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.IService.UserCenter;

namespace Ku.Core.CMS.Web.Backend.Components
{
    [ViewComponent(Name = "AuthCodeJs")]
    public class AuthCodeJsViewComponent : ViewComponent
    {
        private IBackendAuthService service;

        public AuthCodeJsViewComponent(IBackendAuthService _service)
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
