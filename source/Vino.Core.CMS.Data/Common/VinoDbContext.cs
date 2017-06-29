using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Common;
using Vino.Core.CMS.Data.Entity;
using Vino.Core.CMS.Data.Entity.System;
using Microsoft.EntityFrameworkCore;
using Pomelo.AspNetCore.TimedJob.EntityFramework;

namespace Vino.Core.CMS.Data.Common
{
    public class VinoDbContext: DbContext, IDbContext, ITimedJobContext
    {
        public VinoDbContext(DbContextOptions<VinoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //创建UserRole关系
            modelBuilder.Entity<UserRole>() .HasKey(t => new { t.UserId, t.RoleId });
            modelBuilder.Entity<UserRole>()
                   .HasOne(pt => pt.User)
                   .WithMany(p => p.UserRoles)
                   .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<UserRole>()
                   .HasOne(pt => pt.Role)
                   .WithMany(t => t.UserRoles)
                   .HasForeignKey(pt => pt.RoleId);

            //创建RoleMenu关系
            modelBuilder.Entity<RoleMenu>().HasKey(t => new { t.RoleId, t.MenuId });
            modelBuilder.Entity<RoleMenu>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.RoleMenus)
                .HasForeignKey(pt => pt.RoleId);

            base.OnModelCreating(modelBuilder);

            //定时任务相关
            modelBuilder.SetupTimedJobs();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        //定时任务相关
        public DbSet<TimedJob> TimedJobs { get; set; }
    }
}
