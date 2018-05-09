using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ku.Core.CMS.Domain.Entity.System;

namespace Ku.Core.CMS.Data.Common
{
    public class SecondaryVinoDbContext : DbContext, IDbContext
    {
        public static DbContextOptions<SecondaryVinoDbContext> Options;

        public SecondaryVinoDbContext()
            : base(Options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //创建UserRole关系
            modelBuilder.Entity<UserRole>().HasKey(t => new { t.UserId, t.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);

            //创建RoleFunction关系
            modelBuilder.Entity<RoleFunction>().HasKey(t => new { t.RoleId, t.FunctionId });
            modelBuilder.Entity<RoleFunction>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.RoleFunctions)
                .HasForeignKey(pt => pt.RoleId);



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserActionLog> UserActionLogs { get; set; }
    }
}
