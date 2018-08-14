using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.Role
{
    /// <summary>
    /// 角色 授权页面
    /// </summary>
    [Auth("usercenter.role")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class FunctionModel : BasePage
    {
        private readonly IRoleService _service;
        private readonly IFunctionService _service2;
        private readonly IRoleFunctionService _service3;

        public FunctionModel(IRoleService service, IFunctionService service2, IRoleFunctionService service3)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
        }

        public RoleDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long RoleId)
        {
            Dto = await _service.GetByIdAsync(RoleId);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }

        [Auth("function")]
        public async Task<IActionResult> OnGetFunctionListAsync(long RoleId, long? pid)
        {
            //取得角色所有功能列表
            var RoleFunctions = await _service3.GetListAsync(new RoleFunctionSearch { RoleId = RoleId }, null);

            var functions = await _service2.GetSubsAsync(pid);
            foreach (var item in functions)
            {
                if (RoleFunctions.Any(x => x.FunctionId == item.Id))
                {
                    item.IsRoleHasAuth = true;
                }
                else
                {
                    item.IsRoleHasAuth = false;
                }
            }
            return JsonData(functions);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync(long RoleId, long FunctionId, bool HasAuth)
        {
            await _service.SaveRoleAuthAsync(RoleId, FunctionId, HasAuth);

            return Json(true);
        }
    }
}
