using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.App
{
    /// <summary>
    /// 应用 编辑页面
    /// </summary>
    [Auth("datacenter.app")]
    public class EditModel : BasePage
    {
        private readonly IAppService _service;

        public EditModel(IAppService service)
        {
            this._service = service;
        }

        [BindProperty]
        public AppDto Dto { set; get; }

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
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new AppDto();
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
                throw new VinoArgNullException();
            }

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
