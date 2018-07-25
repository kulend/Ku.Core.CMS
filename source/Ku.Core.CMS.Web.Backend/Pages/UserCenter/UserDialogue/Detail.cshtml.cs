using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.UserDialogue
{
    /// <summary>
    /// 用户对话 详情页面
    /// </summary>
    [Auth("usercenter.userdialogue")]
    public class DetailModel : BasePage
    {
        private readonly IUserDialogueService _service;

        public DetailModel(IUserDialogueService service)
        {
            this._service = service;
        }

        public UserDialogueDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }
    }
}
