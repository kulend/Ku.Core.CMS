using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Plugin.Article.Service;

namespace Vino.Core.CMS.Plugin.Article.Controllers
{
    [Area("Article")]
    [Auth("article.article")]
    public class HomeController : BaseController
    {
        IArticleService _service;

        public HomeController(IArticleService service)
        {
            this._service = service;
        }


        [Auth("view")]
        public IActionResult Index()
        {
            Debug.WriteLine("Article/Home/Index");
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows)
        {
            var data = await _service.GetListAsync(page, rows);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}
