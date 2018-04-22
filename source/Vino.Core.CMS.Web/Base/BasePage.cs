using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Base
{
    public class BasePage : PageModel
    {
        protected IActionResult JsonData(object data)
        {
            return new JsonResult(data);
        }

        protected IActionResult Json(object data)
        {
            return new JsonResult(data);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        protected IActionResult PagerData<T>(IEnumerable<T> items, int page, int size, int total)
        {
            var result = new LayuiPagerResult<T>(items, page, size, total);
            return new LayuiJsonResult(result);
        }
    }
}
