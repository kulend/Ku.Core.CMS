using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Domain.Entity.Content;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Album
{
    /// <summary>
    /// 专辑文章列表页面
    /// </summary>
    [Auth("content.album.article")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class AlbumArticleListModel : BasePage
    {
        private readonly IColumnService _columnService;
        private readonly IArticleService _articleService;

        public AlbumArticleListModel(IColumnService columnService, IArticleService articleService)
        {
            _columnService = columnService;
            _articleService = articleService;
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        public void OnGet(long albumId)
        {
            ViewData["AlbumId"] = albumId;
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        [Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, ArticleSearch where)
        {
            var data = await _articleService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}
