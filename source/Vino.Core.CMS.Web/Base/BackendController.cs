using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Extensions;

namespace Vino.Core.CMS.Web.Base
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class BackendController : Controller
    {
        /// <summary>
        /// 是否是Json请求
        /// </summary>
        /// <returns></returns>
        public bool IsJsonRequest()
        {
            return base.HttpContext.Request.IsJsonRequest();
        }

        protected IActionResult JsonData(object data)
        {
            //var result = new JsonResult
            //{
            //    Code = 0,
            //    Data = data
            //};
            return Json(data);
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
