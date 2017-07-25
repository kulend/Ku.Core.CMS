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
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Web.Security;

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
        public async Task<IActionResult> GetList(int page, int rows)
        {
            var data = await service.GetListAsync(page, rows);
            return PagerData(data.items, page, rows, data.count);
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
        public async Task<IActionResult> Save(UserDto model, long[] UserRoles)
        {
            await service.SaveAsync(model, UserRoles);
            return JsonData(true);
        }

        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await service.DeleteAsync(id);
            return JsonData(true);
        }
    }
}
