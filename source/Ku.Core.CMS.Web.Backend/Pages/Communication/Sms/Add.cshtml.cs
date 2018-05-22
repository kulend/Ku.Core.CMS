using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using Ku.Core.CMS.IService.Communication;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Communication.Sms
{
    /// <summary>
    /// 短信 编辑页面
    /// </summary>
    [Auth("communication.sms")]
    public class AddModel : BasePage
    {
        private readonly ISmsService _service;
        private readonly ISmsTempletService _templetService;

        public AddModel(ISmsService service, ISmsTempletService templetService)
        {
            _service = service;
            _templetService = templetService;
        }

        [BindProperty]
        public SmsDto Dto { set; get; }

        public IEnumerable<SmsTempletDto> Templets { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
        {
            Templets = await _templetService.GetListAsync(new SmsTempletSearch { IsDeleted = false }, null);

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
                Dto = new SmsDto();
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

            await _service.AddAsync(Dto);
            return Json(true);
        }
    }
}
