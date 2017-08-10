using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Tools;

namespace Vino.Core.CMS.Web.Admin.Controllers
{
    public class ValidateController: Controller
    {
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ImageCode(string type)
        {
            var ms = new ImageValidateCodeTools().Create(out string code, 4);
            HttpContext.Session.SetString($"ImageValidateCode_{type}", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}
