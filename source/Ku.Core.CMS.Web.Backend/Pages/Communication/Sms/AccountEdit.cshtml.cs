using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.IService.Communication;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.Communication.Sms
{
    /// <summary>
    /// 短信账户 编辑页面
    /// </summary>
    [Auth("communication.sms.account")]
    public class AccountEditModel : BasePage
    {
        private readonly ISmsAccountService _service;

        public AccountEditModel(ISmsAccountService service)
        {
            this._service = service;
        }

        [BindProperty]
        public SmsAccountDto Dto { set; get; }

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
                    throw new VinoDataNotFoundException();
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new SmsAccountDto();
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
