using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Common;
using Vino.Core.CMS.Data.Entity;
using Vino.Core.CMS.Data.Entity.System;
using Microsoft.EntityFrameworkCore;

namespace Vino.Core.CMS.Data.Common
{
    public class VinoDbContext: DbContext, IDbContext
    {
        public VinoDbContext(DbContextOptions<VinoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; }
    }
}
