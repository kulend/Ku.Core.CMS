using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Extensions;

namespace Vino.Core.CMS.Web.Admin.Views.Home
{
    [Area("Default")]
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        [Auth]
        public IActionResult Index()
        {
            ViewData["LoginUserName"] = User.GetUserNameOrNull();
            return View();
        }
    }
}
