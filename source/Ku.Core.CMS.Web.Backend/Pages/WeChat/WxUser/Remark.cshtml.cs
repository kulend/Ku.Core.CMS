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

namespace Ku.Core.CMS.Web.Backend.Pages.WeChat.WxUser
{
    /// <summary>
    /// 微信用户 编辑页面
    /// </summary>
    [Auth("wechat.wxuser")]
    public class RemarkModel : BasePage
    {
        private readonly IWxUserService _service;

        public RemarkModel(IWxUserService service)
        {
            this._service = service;
        }

        [BindProperty]
        public WxUserDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            await _service.SaveRemarkAsync(Dto);
            return Json(true);
        }
    }
}
