using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Frontend.PcSite.Pages.News
{
    public class ListModel : PageModel
    {
        private readonly IColumnService _columnService;
        private readonly IArticleService _articleService;

        public ListModel(IColumnService columnService, IArticleService articleService)
        {
            _columnService = columnService;
            _articleService = articleService;
        }

        public async Task OnGetAsync(long id)
        {
            //取得栏目及子栏目信息
            var columns = await _columnService.GetListFromCacheAsync();
            List<long> columnIds = new List<long>();
            void GetColumnId(long cid)
            {
                var column = columns.SingleOrDefault(x=>x.Id == cid);
                if (column != null)
                {
                    columnIds.Add(column.Id);
                    if (column.Subs != null || column.Subs.Any())
                    {
                        foreach (var item in column.Subs)
                        {
                            GetColumnId(item.Id);
                        }
                    }
                }
            }

            //取得文章列表
            var data = await _articleService.GetListAsync(1, 20, new ArticleSearch { IsPublished = true, IsDeleted = false, ColumnId = id }, null);
        }
    }
}