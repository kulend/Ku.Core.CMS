using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Views.Menu
{
    [Area("System")]
    public class MenuController : BaseController
    {
        private IMenuService service;
        public MenuController(IMenuService _service)
        {
            this.service = _service;
        }

        #region 菜单

        [Authorize]
        public async Task<IActionResult> Index(long? parentId)
        {
            var Parents = new List<MenuDto>();
            if (parentId.HasValue)
            {
                Parents = await service.GetParentsAsync(parentId.Value);
            }
            ViewData["ParentId"] = parentId;
            return View(Parents);
        }

        [Authorize]
        public async Task<IActionResult> List(long? parentId, int page = 1, int rows = 10)
        {
            var data = await service.GetListAsync(parentId, page, rows);
            return PagerData(data.list, page, rows, data.count);
        }

        [Authorize]
        public async Task<IActionResult> Edit(long? id, long? pid)
        {
            if (id.HasValue)
            {
                //编辑
                var module = await service.GetByIdAsync(id.Value);
                if (module == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                if (module.ParentId.HasValue)
                {
                    ViewBag.Parents = await service.GetParentsAsync(module.ParentId.Value);
                }
                ViewData["Mode"] = "Edit";
                return View(module);
            }
            else
            {
                //新增
                MenuDto dto = new MenuDto();
                if (pid.HasValue)
                {
                    dto.ParentId = pid.Value;
                    ViewBag.Parents = await service.GetParentsAsync(pid.Value);
                }
                else
                {
                    dto.ParentId = null;
                }
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Authorize, HttpPost]
        public async Task<IActionResult> Save(MenuDto model)
        {
            await service.SaveAsync(model);
            return JsonData(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [Authorize, HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await service.DeleteAsync(id);
            return JsonData(true);
        }

        #endregion
    }
}
