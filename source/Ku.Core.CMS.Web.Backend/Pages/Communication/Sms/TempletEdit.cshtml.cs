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
    /// 短信模板 编辑页面
    /// </summary>
    [Auth("communication.sms.templet")]
    public class TempletEditModel : BasePage
    {
        private readonly ISmsTempletService _service;
        private readonly ISmsAccountService _accountService;

        public TempletEditModel(ISmsTempletService service, ISmsAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        [BindProperty]
        public SmsTempletDto Dto { set; get; }

        public IEnumerable<SmsAccountDto> Accounts { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
        {
            //取得短信账号列表
            Accounts = await _accountService.GetListAsync(new SmsAccountSearch { IsDeleted = false }, null);

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
                Dto = new SmsTempletDto();
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
