using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.TimedTask.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Ku.Core.CMS.Data.Common
{
    public partial class VinoDbContext: ITimedTaskContext
    {
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

            //菜单
            modelBuilder.Entity<Menu>().HasMany(m => m.SubMenus).WithOne(m => m.Parent);

            base.OnModelCreating(modelBuilder);

            //定时任务相关
            modelBuilder.SetupTimedTask();
        }

        //定时任务相关
        public DbSet<TimedTask.TimedTask> TimedTasks { get; set; }
        public DbSet<TimedTask.TimedTaskLog> TimedTaskLogs { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RoleFunction> RoleFunctions { get; set; }
    }
}
