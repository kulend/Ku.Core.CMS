//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：FunctionController.cs
// 功能描述：功能 后台访问控制类
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
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Areas.System.Views.Function
{
    [Area("System")]
    [Auth("system.function")]
    public class FunctionController : BackendController
    {
        private readonly IFunctionService _service;

        public FunctionController(IFunctionService service)
        {
            this._service = service;
        }


        #region 功能

        [Auth("view")]
        public async Task<IActionResult> Index(long? parentId)
        {
            var Parents = new List<FunctionDto>();
            if (parentId.HasValue)
            {
                Parents = await _service.GetParentsAsync(parentId.Value);
            }
            ViewData["ParentId"] = parentId;
            return View(Parents);
        }

        [Auth("view")]
        public async Task<IActionResult> List(long? parentId, int page = 1, int rows = 10)
        {
            var search = new FunctionSearch();
            search.ParentId = parentId;
            var data = await _service.GetListAsync(page, rows, search, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id, long? pid)
        {
            if (id.HasValue)
            {
                //编辑
                var module = await _service.GetByIdAsync(id.Value);
                if (module == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                if (module.ParentId.HasValue)
                {
                    ViewBag.Parents = await _service.GetParentsAsync(module.ParentId.Value);
                }
                ViewData["Mode"] = "Edit";
                return View(module);
            }
            else
            {
                //新增
                FunctionDto dto = new FunctionDto();
                dto.IsEnable = true;
                if (pid.HasValue)
                {
                    dto.ParentId = pid.Value;
                    ViewBag.Parents = await _service.GetParentsAsync(pid.Value);
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
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(FunctionDto model)
        {
            await _service.SaveAsync(model);
            return JsonData(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(params long[] id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("restore")]
        public async Task<IActionResult> Restore(params long[] id)
        {
            await _service.RestoreAsync(id);
            return JsonData(true);
        }


        #endregion
    }
}
