using System;
using System.Collections.Generic;
using System.Text;
using Ku.Core.CMS.Core.Common;
using Ku.Core.CMS.Data.Entity;
using Ku.Core.CMS.Data.Entity.System;
using Microsoft.EntityFrameworkCore;

namespace Ku.Core.CMS.Data.Common
{
    public class KuDbContext: DbContext, IDbContext
    {
        public KuDbContext(DbContextOptions<KuDbContext> options)
            : base(options)
        {
        }

        public DbSet<Operator> Operators { get; set; }
    }
}
