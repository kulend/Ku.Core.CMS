using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Web.Admin.Controllers.System
{
    public class MenuController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult GetMenuByParentId(long pid)
        {
            var service = IoC.Resolve<IMenuService>();
            var menus = service.GetMenusByParentId(pid);
            return Json(new {code = 0, data = menus});
        }

        [Authorize]
        public IActionResult Edit(long? id, long? pid)
        {
            var service = IoC.Resolve<IMenuService>();
            if (id.HasValue)
            {
                //编辑
                var menu = service.GetById(id.Value);
                if (menu == null)
                {
                    throw new VinoDataNotFoundException("无法取得菜单数据!");
                }
                if (menu.ParentId == 0)
                {
                    ViewData["ParentMenuName"] = "顶级菜单";
                }
                else
                {
                    var parentMenu = service.GetById(menu.ParentId);
                    if (parentMenu == null)
                    {
                        throw new VinoDataNotFoundException("无法取得上级菜单数据!");
                    }
                    ViewData["ParentMenuName"] = parentMenu.Name;
                }
                ViewData["Mode"] = "Edit";
                return View(menu);
            }
            else
            {
                //新增
                MenuDto dto = new MenuDto();
                dto.IsShow = true;
                if (pid.HasValue)
                {
                    var parentMenu = service.GetById(pid.Value);
                    if (parentMenu == null)
                    {
                        throw new VinoDataNotFoundException("无法取得上级菜单数据!");
                    }
                    ViewData["ParentMenuName"] = parentMenu.Name;
                    dto.ParentId = parentMenu.Id;
                }
                else
                {
                    dto.ParentId = 0;
                    ViewData["ParentMenuName"] = "顶级菜单";
                }
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        [Authorize, HttpPost]
        public IActionResult Save(MenuDto model)
        {
            var service = IoC.Resolve<IMenuService>();
            service.SaveMenu(model);
            return Json(new { code = 0 });
        }
    }
}
