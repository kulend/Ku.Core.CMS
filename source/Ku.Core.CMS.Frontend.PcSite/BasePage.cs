using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Frontend.PcSite
{
    public class BasePcPage : PageModel
    {
        protected IActionResult Json(object data)
        {
            return new JsonResult(data);
        }
    }

    /// <summary>
    /// 需要登陆的页面
    /// </summary>
    [Authorize]
    public class LoginAuthPage : BasePcPage
    {

    }
}
