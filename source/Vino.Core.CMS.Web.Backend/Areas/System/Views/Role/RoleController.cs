//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：RoleController.cs
// 功能描述：角色 后台访问控制类
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
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Web.Backend.Areas.System.Views.Role
{
    [Area("System")]
    [Auth("system.role")]
    public class RoleController : BackendController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            this._service = service;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, RoleSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                RoleDto dto = new RoleDto();
                dto.IsEnable = true;
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(RoleDto model)
        {
            await _service.SaveAsync(model);
            return JsonData(true);
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


        [Auth("function")]
        public IActionResult RoleFunction(long RoleId)
        {
            ViewData["RoleId"] = RoleId;
            return View();
        }

        [Auth("function")]
        public async Task<IActionResult> GetFunctionsWithRoleAuth(long RoleId, long? pid)
        {
            var functions = await _service.GetFunctionsWithRoleAuthAsync(RoleId, pid);
            return JsonData(functions);
        }

        [Auth("function")]
        public async Task<IActionResult> SaveRoleAuth(long RoleId, long FunctionId, bool HasAuth)
        {
            await _service.SaveRoleAuthAsync(RoleId, FunctionId, HasAuth);
            return JsonData(true);
        }
    }
}
