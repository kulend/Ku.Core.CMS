using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.UserDialogue
{
    /// <summary>
    /// 用户对话 详情页面
    /// </summary>
    [Auth("usercenter.user.dialogue")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class DetailModel : BasePage
    {
        private readonly IUserDialogueService _service;
        private readonly IUserDialogueMessageService _service2;

        public DetailModel(IUserDialogueService service, IUserDialogueMessageService service2)
        {
            _service = service;
            _service2 = service2;
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

        [Auth("view")]
        public async Task<IActionResult> OnGetMessageListAsync(long id)
        {
            var data = await _service2.GetListAsync(1, 20, new UserDialogueMessageSearch { DialogueId = id, IsDeleted = false }, new { CreateTime = "desc" });
            return Json(data.items.OrderBy(x=>x.CreateTime));
        }

        [Auth("reply")]
        public async Task<IActionResult> OnPostReplyAsync(long id, string content)
        {
            await _service.AdminReplyAsync(id, content, User.GetUserIdOrZero());
            return Json(true);
        }

        [Auth("forbidden")]
        public async Task<IActionResult> OnPostForbiddenAsync(long id, bool status)
        {
            await _service.AdminForbiddenAsync(id, status);
            return Json(true);
        }

        [Auth("solve")]
        public async Task<IActionResult> OnPostSolveAsync(long id)
        {
            await _service.AdminSolveAsync(id, User.GetUserIdOrZero());
            return Json(true);
        }
    }
}
