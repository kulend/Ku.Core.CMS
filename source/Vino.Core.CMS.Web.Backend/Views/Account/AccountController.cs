using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Web.Backend.Models;

namespace Vino.Core.CMS.Web.Admin.Views.Account
{
    public class AccountController : BaseController
    {
        private IUserService userService;

        public AccountController(IUserService _userService)
        {
            this.userService = _userService;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        [Auth]
        public IActionResult PasswordEdit()
        {
            return View();
        }

        /// <summary>
        /// 保存密码
        /// </summary>
        [Auth]
        [UserAction("修改密码")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePassword(PwdData data)
        {
            if (!data.NewPassword.Equals(data.ConfirmPassword))
            {
                throw new VinoArgNullException("两次输入的密码不一致！");
            }
            await userService.ChangePassword(base.User.GetUserIdOrZero(), data.CurrentPassword, data.NewPassword);

            await HttpContext.Authentication.SignOutAsync("Cookie");

            return Json(true);
        }
    }
}
