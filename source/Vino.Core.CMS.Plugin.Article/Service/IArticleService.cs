using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Plugin.Article.Models.Dto;

namespace Vino.Core.CMS.Plugin.Article.Service
{
    public interface IArticleService
    {
        Task<(int count, List<ArticleDto> items)> GetListAsync(int page, int size);
    }
}
