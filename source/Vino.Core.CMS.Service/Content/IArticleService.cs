using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Content;
using Vino.Core.CMS.Domain.Dto.Content;

namespace Vino.Core.CMS.Service.Content
{
    public partial interface IArticleService
    {
        Task<(int count, List<ArticleDto> items)> GetListAsync(int page, int size);

        Task<ArticleDto> GetByIdAsync(long id);

        Task SaveAsync(ArticleDto dto);

        Task DeleteAsync(long id);
    }
}
