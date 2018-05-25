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
    /// 微信用户 详情页面
    /// </summary>
    [Auth("wechat.wxuser")]
    public class DetailModel : BasePage
    {
        private readonly IWxUserService _service;

        public DetailModel(IWxUserService service)
        {
            this._service = service;
        }

        public WxUserDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new VinoDataNotFoundException();
            }
        }
    }
}
