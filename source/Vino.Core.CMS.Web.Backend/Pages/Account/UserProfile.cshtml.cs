using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Pages.Account
{
    [Auth]
    public class UserProfileModel : BasePage
    {
        private IUserService _userService;

        public UserProfileModel(IUserService userService)
        {
            this._userService = userService;
        }

        [BindProperty]
        [Required, StringLength(20)]
        [Display(Name = "当前密码")]
        [DataType(DataType.Password)]
        public string CurrentPassword { set; get; }

        [BindProperty]
        [Required, StringLength(20)]
        [Display(Name = "新密码", Description = "长度在6~20之间，只能包含字母、数字和下划线")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9]\w{5,19}$", ErrorMessage = "输入的密码不符合规则")]
        public string NewPassword { set; get; }

        [BindProperty]
        [Required, StringLength(20)]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [RegularExpression(@"custom|$('input[name=NewPassword]').val() === value", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { set; get; }

        public void OnGet()
        {

        }

        [UserAction("修改密码")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new VinoArgNullException();
            }

            if (!NewPassword.Equals(ConfirmPassword))
            {
                throw new VinoArgNullException("两次输入的密码不一致！");
            }
            await _userService.ChangePassword(User.GetUserIdOrZero(), CurrentPassword, NewPassword);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Json(true);
        }
    }
}