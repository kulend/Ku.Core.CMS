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
    public class HomeController : BackendController
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

        [Auth]
        public IActionResult LayuiAdmin()
        {
            ViewData["LoginUserName"] = User.GetUserNameOrNull();
            return View();
        }
    }
}
