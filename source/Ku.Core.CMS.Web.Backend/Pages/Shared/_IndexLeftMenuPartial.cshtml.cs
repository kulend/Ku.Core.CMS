using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.IService.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages.Shared
{
    public class _IndexLeftMenuPartialModel : PageModel
    {
        private IMenuService service;

        public _IndexLeftMenuPartialModel(IMenuService _service)
        {
            this.service = _service;
        }

        public List<MenuDto> Menus { set; get; }

        public async Task OnGetAsync()
        {
            Menus = await service.GetMenuTreeAsync();
        }
    }
}