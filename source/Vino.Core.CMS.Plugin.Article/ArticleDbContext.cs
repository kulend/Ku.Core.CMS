using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Data.Common;

namespace Vino.Core.CMS.Plugin.Article
{
    public class ArticleDbContext : VinoDbContext
    {
        public DbSet<Models.Article> Articles { get; set; }

        public ArticleDbContext(DbContextOptions<VinoDbContext> options) : base(options)
        {
        }
    }

    public class ArticleDbContext2 : DbContext
    {
        public DbSet<Models.Article> Articles { get; set; }

        public ArticleDbContext2(DbContextOptions<ArticleDbContext2> options)
            : base(options)
        {
        }
    }
}
