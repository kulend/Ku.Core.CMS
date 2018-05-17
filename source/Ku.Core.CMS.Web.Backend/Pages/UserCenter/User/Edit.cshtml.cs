using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.User
{
    /// <summary>
    /// 用户 编辑页面
    /// </summary>
    [Auth("usercenter.user")]
    public class EditModel : BasePage
    {
        private readonly IUserService _service;
        private readonly IRoleService _svcRole;

        public EditModel(IUserService service, IRoleService svcRole)
        {
            _service = service;
            _svcRole = svcRole;
        }

        [BindProperty]
        public UserDto Dto { set; get; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public IEnumerable<RoleDto> Roles { get; set; }

        [BindProperty]
        public long[] UserRoles { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? pid)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new VinoDataNotFoundException();
                }
                Dto.Password = "********************";
                //取得用户角色列表
                if (Dto.IsAdmin)
                {
                    UserRoles = (await _service.GetUserRolesAsync(Dto.Id)).Select(x => x.Id).ToArray();
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new UserDto();
                ViewData["Mode"] = "Add";
            }

            //取得角色列表
            Roles = await _svcRole.GetListAsync(new RoleSearch { IsDeleted = false }, null);

            if (UserRoles == null)
            {
                UserRoles = new long[] { };
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new VinoArgNullException();
            }

            await _service.SaveAsync(Dto, UserRoles);
            return Json(true);
        }
    }
}
