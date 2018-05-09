//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MenuController.cs
// 功能描述：菜单 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 20:18
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Areas.System.Views.Menu
{
    [Area("System")]
    [Auth("system.menu")]
    public class MenuController : BackendController
    {
        private IMenuService service;
        public MenuController(IMenuService _service)
        {
            this.service = _service;
        }

        #region 菜单

        [Auth("view")]
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

        [Auth("view")]
        public async Task<IActionResult> List(long? parentId, int page = 1, int rows = 10)
        {
            var search = new MenuSearch();
            search.ParentId = parentId;
            var data = await service.GetListAsync(page, rows, search, "OrderIndex asc");
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
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
        [Auth("edit"), HttpPost]
        public async Task<IActionResult> Save(MenuDto model)
        {
            await service.SaveAsync(model);
            return JsonData(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [Auth("delete"), HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await service.DeleteAsync(id);
            return JsonData(true);
        }

        #endregion
    }
}
