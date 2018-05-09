using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Service.System;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.IService.System;

namespace Ku.Core.CMS.Web.Backend.Components
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
