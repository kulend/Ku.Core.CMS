using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.Domain.Enum.UserCenter;
using System.ComponentModel.DataAnnotations;
using Ku.Core.CMS.Web.Extensions;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.UserPoint
{
    /// <summary>
    /// 用户积分 编辑页面
    /// </summary>
    [Auth("usercenter.userpoint")]
    public class AdjustModel : BasePage
    {
        private readonly IUserPointService _service;

        public AdjustModel(IUserPointService service)
        {
            this._service = service;
        }

        [BindProperty]
        public UserPointAdjustDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(EmUserPointType? type)
        {
            Dto = new UserPointAdjustDto();
            Dto.Type = type ?? EmUserPointType.Mall;
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

            await _service.AdjustAsync(Dto.UserId, Dto.Type, Dto.Points,
                EmUserPointBusType.AdminAdjust, 0, Dto.Remark, User.GetUserIdOrZero());

            return Json(true);
        }
    }

    public class UserPointAdjustDto
    {
        /// <summary>
        /// 积分类型
        /// </summary>
        [Display(Name = "积分类型")]
        public EmUserPointType Type { set; get; } = EmUserPointType.Mall;

        /// <summary>
        /// 会员ID
        /// </summary>
        public long[] UserId { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "变动积分", Description = "输入负值则进行积分扣除")]
        [Range(-9999, 9999)]
        public int Points { get; set; }

        /// <summary>
        /// 业务备注
        /// </summary>
        [StringLength(200)]
        [Display(Name = "业务备注", Prompt = "请备注此次积分变动的原因")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
    }

}
