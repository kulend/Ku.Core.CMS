using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Article
{
    /// <summary>
    /// 文章 列表页面
    /// </summary>
    [Auth("content.article")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : BasePage
    {
        private readonly IColumnService _columnService;
        private readonly IArticleService _articleService;

        public IndexModel(IColumnService columnService, IArticleService articleService)
        {
            _columnService = columnService;
            _articleService = articleService;
        }

        public IEnumerable<ColumnDto> Columns { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(string tag = "")
        {
            long? parentId = null;
            if (!string.IsNullOrEmpty(tag))
            {
                var column = await _columnService.GetOneAsync(new ColumnSearch { IsDeleted = false, Tag = tag });
                parentId = column?.Id;
            }
            Columns = await _columnService.GetListAsync(new ColumnSearch { IsDeleted = false, ParentId = parentId }, "OrderIndex asc");
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

        /// <summary>
        /// 删除
        /// </summary>
        [Auth("delete")]
        public async Task<IActionResult> OnPostDeleteAsync(params long[] id)
        {
            await _articleService.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [Auth("restore")]
        public async Task<IActionResult> OnPostRestoreAsync(params long[] id)
        {
            await _articleService.RestoreAsync(id);
            return JsonData(true);
        }
    }
}
