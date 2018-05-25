using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages.WeChat.WxUser
{
    public class UserSyncModel : BasePage
    {
        private readonly IWxUserService _service;
        private readonly IWxAccountService _accountService;

        public UserSyncModel(IWxUserService service, IWxAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        [BindProperty]
        public WxAccountDto Account { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long AccountId)
        {
            Account =  await _accountService.GetByIdAsync(AccountId);
            if (Account == null)
            {
                throw new VinoDataNotFoundException();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            await _service.SyncAsync(Account.Id);
            return Json(true);
        }
    }
}