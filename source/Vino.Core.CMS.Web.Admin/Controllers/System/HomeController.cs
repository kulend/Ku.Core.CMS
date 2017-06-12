using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vino.Core.CMS.Web.Admin.Controllers.System
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewData["LoginUserName"] = identity.FindFirst(ClaimTypes.Name).Value;
            return View();
        }
    }
}
