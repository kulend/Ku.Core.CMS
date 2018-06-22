using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages.UserCenter.User
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SelectorModel : BasePage
    {
        private readonly IUserService _service;

        public SelectorModel(IUserService service)
        {
            this._service = service;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        //[Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, UserSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}