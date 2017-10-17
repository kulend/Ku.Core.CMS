using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Common;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Plugin.Article.Repository
{
    /// <summary>
    /// 文章仓储接口
    /// </summary>
    public partial interface IArticleRepository : IRepository<Models.Article>
    {
    }

    /// <summary>
    /// 文章仓储接口实现
    /// </summary>
    public partial class ArticleRepository : BaseRepository<Models.Article>, IArticleRepository
    {
        public ArticleRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
