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

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.UserAddress
{
    /// <summary>
    /// 收货地址 编辑页面
    /// </summary>
    [Auth("usercenter.user.address")]
    public class EditModel : BasePage
    {
        private readonly IUserAddressService _service;
        private readonly IUserService _userService;

        public EditModel(IUserAddressService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [BindProperty]
        public UserAddressDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                //取得用户信息
                Dto.User = await _userService.GetByIdAsync(Dto.UserId);
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new UserAddressDto();
                ViewData["Mode"] = "Add";
            }
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
