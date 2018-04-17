//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：UserController.cs
// 功能描述：用户 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 20:18
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Web.Backend.Areas.System.Views.User
{
    [Area("System")]
    [Auth("system.user")]
    public class UserController : BackendController
    {
        private IUserService _service;
        private IRoleService roleService;
        public UserController(IUserService service, IRoleService _roleService)
        {
            this._service = service;
            this.roleService = _roleService;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, UserSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            //取得角色列表
            var roles = await roleService.GetListAsync(null, null);
            ViewBag.Roles = roles;

            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据!");
                }
                model.Password = "********************";
                var userRoles = await _service.GetUserRolesAsync(model.Id);
                ViewBag.UserRoles = userRoles;
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                ViewBag.UserRoles = new List<RoleDto>();
                //新增
                UserDto dto = new UserDto();
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
        [UserAction("编辑用户")]
        public async Task<IActionResult> Save(UserDto model, long[] UserRoles)
        {
            await _service.SaveAsync(model, UserRoles);
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
    }
}
