using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.CMS.Web.Backend.Models;
using Microsoft.AspNetCore.Http;

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
            var user = new LoginUser
            {
                Name = User.GetUserNameOrNull(),
                HeadImage = User.GetHeadImage()
            };

            ViewBag.LoginUser = user;
            return View();
        }
    }
}
