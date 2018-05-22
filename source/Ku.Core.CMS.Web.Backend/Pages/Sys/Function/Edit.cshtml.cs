using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Sys.Function
{
    /// <summary>
    /// 功能 编辑页面
    /// </summary>
    [Auth("system.function")]
    public class EditModel : BasePage
    {
        private readonly IFunctionService _service;

        public EditModel(IFunctionService service)
        {
            this._service = service;
        }

        [BindProperty]
        public FunctionDto Dto { set; get; }

        public List<FunctionDto> Parents { set; get; }

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
                if (Dto.ParentId.HasValue)
                {
                    Parents = await _service.GetParentsAsync(Dto.ParentId.Value);
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new FunctionDto();
                if (pid.HasValue)
                {
                    Dto.ParentId = pid.Value;
                    Parents = await _service.GetParentsAsync(pid.Value);
                }
                else
                {
                    Dto.ParentId = null;
                }
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
