using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;

namespace Vino.Core.CMS.Web.Backend.Areas.System.Views.Location
{
    [Area("System")]
    [Auth("system.location")]
    public class LocationController : BackendController
    {
        /// <summary>
        /// 省份选择页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ProvinceSelect()
        {
            return View();
        }
    }
}
