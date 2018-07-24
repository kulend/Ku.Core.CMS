using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.IService.UserCenter;
using System.Linq;

namespace Ku.Core.CMS.Web.Backend.Components
{
    [ViewComponent(Name = "HomeMenu")]
    public class HomeMenuViewComponent : ViewComponent
    {
        private IMenuService service;
        private IBackendAuthService authService;

        public HomeMenuViewComponent(IMenuService service, IBackendAuthService authService)
        {
            this.service = service;
            this.authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? pid)
        {
            var userId = HttpContext.User.GetUserIdOrZero();
            if (userId == 0)
            {
                return View(new List<MenuDto>());
            }

            //取得所有authcode
            var codes = await authService.GetUserAuthCodesAsync(userId, false);

            List<MenuDto> list = await service.GetMenuTreeAsync();

            void CheckAuth(MenuDto menu)
            {
                if (string.IsNullOrEmpty(menu.AuthCode) || codes.Contains("ku.develop") || codes.Contains(menu.AuthCode))
                {
                    menu.HasAuth = true;
                }
                else
                {
                    menu.HasAuth = false;
                }

                foreach (var sub in menu.SubMenus)
                {
                    CheckAuth(sub);
                }
            }

            list.ForEach(x => CheckAuth(x));
            return View(list);
        }
    }
}
