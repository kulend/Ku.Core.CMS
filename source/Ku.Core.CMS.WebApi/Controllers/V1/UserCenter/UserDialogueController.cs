using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Api.Throttle;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ku.Core.CMS.WebApi.Controllers.V1.UserCenter
{
    /// <summary>
    /// 用户留言香港接口
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/user/dialogue")]
    [UserAuth]
    public class UserDialogueController : Controller
    {
        private readonly IUserDialogueService _service;

        public UserDialogueController(IUserDialogueService service)
        {
            _service = service;
        }

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="message">内容</param>
        [HttpPost]
        [ApiThrottle(Limit =1, Duration = 30, BasisCondition = BasisCondition.UserIdentity)]
        public async Task<IActionResult> PostAsync([StringLength(500, ErrorMessage = "aaa", MinimumLength = 10), Required]string message)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            await _service.AddAsync(User.GetUserIdOrZero(), message);
            return Json(true);
        }
    }
}
