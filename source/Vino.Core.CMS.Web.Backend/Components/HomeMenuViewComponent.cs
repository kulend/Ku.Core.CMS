using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Web.Admin.Components
{
    [ViewComponent(Name = "HomeMenu")]
    public class HomeMenuViewComponent : ViewComponent
    {
        private IMenuService service;

        public HomeMenuViewComponent(IMenuService _service)
        {
            this.service = _service;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? pid)
        {
            List<MenuDto> list = await service.GetMenuTreeAsync();
            return View(list);
        }
    }
}
