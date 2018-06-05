using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Domain.Dto.UserCenter;

namespace Ku.Core.CMS.Web.Backend.Pages.Account
{
    [Auth]
    public class ProfileModel : BasePage
    {
        private IUserService _userService;

        public ProfileModel(IUserService userService)
        {
            this._userService = userService;
        }

        [BindProperty]
        public UserDto Dto { set; get; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.GetUserIdOrZero();
            Dto = await _userService.GetByIdAsync(userId);
            if (Dto == null)
            {
                throw new KuDataNotFoundException("无法取得用户数据！");
            }
            Dto.Password = "******";

            return Page();
        }

        [UserOperationAttribute("修改账户资料")]
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Dto.Account");
            ModelState.Remove("Dto.Password");
            if (!ModelState.IsValid)
            {
                throw new KuArgNullException();
            }

            var userId = User.GetUserIdOrZero();
            if (userId != Dto.Id)
            {
                throw new KuDataNotFoundException("无法取得用户数据！");
            }
            await _userService.SaveProfileAsync(Dto);
            return Json(true);
        }
    }
}