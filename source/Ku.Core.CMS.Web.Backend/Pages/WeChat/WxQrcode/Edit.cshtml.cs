using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.WeChat.WxQrcode
{
    /// <summary>
    /// 微信二维码 编辑页面
    /// </summary>
    [Auth("wechat.wxqrcode")]
    public class EditModel : BasePage
    {
        private readonly IWxQrcodeService _service;
        private readonly IWxAccountService _accountService;

        public EditModel(IWxQrcodeService service, IWxAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        [BindProperty]
        public WxQrcodeDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? AccountId)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new VinoDataNotFoundException();
                }
                Dto.Account = await _accountService.GetByIdAsync(Dto.AccountId);
                if (Dto.Account == null)
                {
                    throw new VinoDataNotFoundException("数据出错!");
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new WxQrcodeDto();
                if (AccountId.HasValue)
                {
                    Dto.AccountId = AccountId.Value;
                    Dto.Account = await _accountService.GetByIdAsync(AccountId.Value);
                    if (Dto.Account == null)
                    {
                        throw new VinoDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    throw new VinoDataNotFoundException("参数出错!");
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
