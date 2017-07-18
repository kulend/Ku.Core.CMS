using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Vino.Core.CMS.Web.Base
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class BaseController : Controller
    {
        protected IActionResult JsonData(object data)
        {
            var result = new JsonResult
            {
                Code = 0,
                Data = data
            };
            return Json(result);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        protected IActionResult PagerData<T>(IEnumerable<T> items, int page, int size, int total)
        {
            var result = new PagerResult<T>(items, page, size, total);
            return JsonData(result);
        }
    }
}
