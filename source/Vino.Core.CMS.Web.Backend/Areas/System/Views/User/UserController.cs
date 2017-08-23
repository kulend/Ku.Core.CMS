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

namespace Vino.Core.CMS.Web.Admin.Areas.System.Views.User
{
    [Area("System")]
    [Auth("sys.user")]
    public class UserController : BaseController
    {
        private IUserService service;
        private IRoleService roleService;
        public UserController(IUserService _service, IRoleService _roleService)
        {
            this.service = _service;
            this.roleService = _roleService;
        }

        [Auth("view")]
        public IActionResult Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int limit)
        {
            var data = await service.GetListAsync(page, limit);
            return PagerData(data.items, page, limit, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            //取得角色列表
            var roles = await roleService.GetAllAsync();
            ViewBag.Roles = roles;

            if (id.HasValue)
            {
                //编辑
                var model = await service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据!");
                }
                model.Password = "the password has not changed";
                var userRoles = await service.GetUserRolesAsync(model.Id);
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
            await service.SaveAsync(model, UserRoles);
            return JsonData(true);
        }

        [HttpPost]
        [Auth("delete")]
        [UserAction("删除用户")]
        public async Task<IActionResult> Delete(long id)
        {
            await service.DeleteAsync(id);
            return JsonData(true);
        }
    }
}
