using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Web.Base;

namespace Ku.Core.CMS.WebApi.Controllers.V1.DataCenter
{
    /// <summary>
    /// 应用相关接口
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/datacenter/appaa")]
    public class AppaaController : WebApiController
    {
        /// <summary>
        /// 取得应用版本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public async Task<JsonResult> Version()
        {
            return Json(true);
        }
    }
}