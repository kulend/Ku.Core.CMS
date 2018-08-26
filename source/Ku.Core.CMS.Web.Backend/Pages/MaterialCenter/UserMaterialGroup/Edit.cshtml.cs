using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.Domain.Enum.MaterialCenter;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.UserMaterialGroup
{
    /// <summary>
    /// 用户素材分组 编辑页面
    /// </summary>
    [Auth("materialcenter.usermaterialgroup")]
    public class EditModel : BasePage
    {
        private readonly IUserMaterialGroupService _service;

        public EditModel(IUserMaterialGroupService service)
        {
            this._service = service;
        }

        [BindProperty]
        public UserMaterialGroupDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, EmUserMaterialGroupType type = EmUserMaterialGroupType.Picture)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new UserMaterialGroupDto();
                Dto.Type = type;
                ViewData["Mode"] = "Add";
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
                throw new KuArgNullException();
            }

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
