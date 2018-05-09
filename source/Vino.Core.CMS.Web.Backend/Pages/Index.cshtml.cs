using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ku.Core.CMS.Web.Backend.Models;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Pages
{
    [Auth]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["LoginUserName"] = User.GetUserNameOrNull();
        }
    }
}