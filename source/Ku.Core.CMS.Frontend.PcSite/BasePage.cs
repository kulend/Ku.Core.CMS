using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Frontend.PcSite
{
    public class BasePage : PageModel
    {

    }

    /// <summary>
    /// 需要登陆的页面
    /// </summary>
    [Authorize]
    public class LoginAuthPage : BasePage
    {

    }
}
