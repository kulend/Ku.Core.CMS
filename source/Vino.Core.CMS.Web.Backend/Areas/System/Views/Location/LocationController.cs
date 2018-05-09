using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;

namespace Ku.Core.CMS.Web.Backend.Areas.System.Views.Location
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
