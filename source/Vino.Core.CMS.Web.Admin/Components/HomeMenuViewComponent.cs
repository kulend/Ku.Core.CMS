using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Web.Admin.Components
{
    [ViewComponent(Name = "HomeMenu")]
    public class HomeMenuViewComponent : ViewComponent
    {
        public HomeMenuViewComponent()
        {

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
                var service = IoC.Resolve<IMenuService>();
                return service.GetMenusByParentId(parentId);
            }

            return Task.FromResult(GetItems(pid));
        }

    }
}
