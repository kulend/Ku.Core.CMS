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

namespace Ku.Core.CMS.Web.Backend.Pages.WeChat.WxMenu
{
    /// <summary>
    /// 微信菜单 编辑页面
    /// </summary>
    [Auth("wechat.wxmenu")]
    public class EditModel : BasePage
    {
        private readonly IWxMenuService _service;
        private readonly IWxAccountService _accountService;
        private readonly IWxUserTagService _userTagService;

        public EditModel(IWxMenuService service, IWxAccountService accountService, IWxUserTagService userTagService)
        {
            _service = service;
            _accountService = accountService;
            _userTagService = userTagService;
        }

        [BindProperty]
        public WxMenuDto Dto { set; get; }

        public IEnumerable<WxUserTagDto> UserTags { set; get; }

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
                    throw new KuDataNotFoundException();
                }
                Dto.Account = await _accountService.GetByIdAsync(Dto.AccountId);
                if (Dto.Account == null)
                {
                    throw new KuDataNotFoundException("数据出错!");
                }
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new WxMenuDto();
                if (AccountId.HasValue)
                {
                    Dto.AccountId = AccountId.Value;
                    Dto.Account = await _accountService.GetByIdAsync(AccountId.Value);
                    if (Dto.Account == null)
                    {
                        throw new KuDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    throw new KuDataNotFoundException("参数出错!");
                }
                ViewData["Mode"] = "Add";
            }

            //取得用户标签
            UserTags = await _userTagService.GetListAsync(new WxUserTagSearch() { AccountId = Dto.AccountId }, new { TagId = "asc" });
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
