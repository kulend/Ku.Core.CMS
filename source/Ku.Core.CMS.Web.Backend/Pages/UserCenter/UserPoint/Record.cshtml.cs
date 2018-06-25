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
using Ku.Core.CMS.Domain.Entity.UserCenter;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.UserPoint
{
    /// <summary>
    /// 用户积分 编辑页面
    /// </summary>
    [Auth("usercenter.userpoint")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class RecordModel : BasePage
    {
        private readonly IUserPointRecordService _service;

        public RecordModel(IUserPointRecordService service)
        {
            this._service = service;
        }


        /// <summary>
        /// 取得数据
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, UserPointRecordSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}
