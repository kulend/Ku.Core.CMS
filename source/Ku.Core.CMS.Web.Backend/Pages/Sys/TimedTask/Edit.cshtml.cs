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

namespace Ku.Core.CMS.Web.Backend.Pages.System.TimedTask
{
    /// <summary>
    /// 定时任务 编辑页面
    /// </summary>
    [Auth("system.timedtask")]
    public class EditModel : BasePage
    {
        private readonly ITimedTaskService _service;

        public EditModel(ITimedTaskService service)
        {
            this._service = service;
        }

        [BindProperty]
        public TimedTaskDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
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
                Dto = new TimedTaskDto();
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
