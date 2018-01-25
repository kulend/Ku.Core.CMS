using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Views.Function
{
    [Area("System")]
    [Auth("sys.function")]
    public class FunctionController : BackendController
    {
        private IFunctionService service;
        public FunctionController(IFunctionService _service)
        {
            this.service = _service;
        }


        #region 功能

        [Auth("view")]
        public async Task<IActionResult> Index(long? parentId)
        {
            var Parents = new List<FunctionDto>();
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
            var data = await service.GetListAsync(parentId, page, rows);
            return PagerData(data.list, page, rows, data.count);
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
                FunctionDto dto = new FunctionDto();
                dto.IsEnable = true;
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
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(FunctionDto model)
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
