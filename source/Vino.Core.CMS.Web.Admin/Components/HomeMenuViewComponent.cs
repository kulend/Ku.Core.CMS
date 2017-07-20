using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Web.Admin.Components
{
    [ViewComponent(Name = "HomeMenu")]
    public class HomeMenuViewComponent : ViewComponent
    {
        private IIocResolver _ioc;

        public HomeMenuViewComponent(IIocResolver ioc)
        {
            this._ioc = ioc;
        }

        public async Task<IViewComponentResult> InvokeAsync(MenuDto pDto)
        {
            List<MenuDto> list;
            var layout = "Default";
            if (pDto == null)
            {
                 list = await GetItemsAsync(0);
            }
            else
            {
                list = await GetItemsAsync(pDto.Id);
                layout = "Sub";
            }
            return View(layout, list);
        }

        private Task<List<MenuDto>> GetItemsAsync(long pid)
        {
            List<MenuDto> GetItems(long parentId)
            {
                var service = _ioc.Resolve<IMenuService>();
                return service.GetMenusByParentId(parentId);
            }

            return Task.FromResult(GetItems(pid));
        }

    }
}
