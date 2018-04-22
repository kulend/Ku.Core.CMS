using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vino.Core.CMS.Web.Backend.Models;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Web.Security;

namespace Vino.Core.CMS.Web.Backend.Pages
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