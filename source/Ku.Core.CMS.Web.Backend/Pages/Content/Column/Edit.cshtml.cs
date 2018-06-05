using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Column
{
    /// <summary>
    /// 详情页面
    /// </summary>
    [Auth("content.column")]
    public class EditModel : BasePage
    {
        private readonly IColumnService _service;

        public EditModel(IColumnService service)
        {
            this._service = service;
        }

        [BindProperty]
        public ColumnDto Dto { set; get; }

        public List<ColumnDto> Parents { set; get; }

        public async Task OnGetAsync(long? id, long? pid)
        {
            if (id.HasValue)
            {
                //编辑
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException("无法取得数据!");
                }
                if (Dto.ParentId.HasValue)
                {
                    Parents = await _service.GetParentsAsync(Dto.ParentId.Value);
                }

                ViewData["Mode"] = "Edit";
            }
            else
            {
                //新增
                Dto = new ColumnDto();
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