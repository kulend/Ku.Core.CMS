using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Enum;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    [DbContext(typeof(VinoDbContext))]
    [Migration("20170804092227_25")]
    partial class _25
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.Membership.Member", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int?>("Factor");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("LastLoginIp")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<int>("Level");

                    b.Property<long?>("MemberTypeId");

                    b.Property<string>("Mobile")
                        .HasMaxLength(11);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("Points");

                    b.Property<short>("Sex");

                    b.HasKey("Id");

                    b.HasIndex("MemberTypeId");

                    b.ToTable("membership_member");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.Membership.MemberType", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("OrderIndex");

                    b.HasKey("Id");

                    b.ToTable("membership_member_type");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.Function", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("AuthCode")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("HasSub");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("system_function");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.Menu", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("AuthCode")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("HasSubMenu");

                    b.Property<string>("Icon")
                        .HasMaxLength(20);

                    b.Property<bool>("IsShow");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("OrderIndex");

                    b.Property<long?>("ParentId");

                    b.Property<string>("Url")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("system_menu");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.Role", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("system_role");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.RoleFunction", b =>
                {
                    b.Property<long>("RoleId");

                    b.Property<long>("FunctionId");

                    b.HasKey("RoleId", "FunctionId");

                    b.HasIndex("FunctionId");

                    b.ToTable("system_role_function");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.User", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int?>("Factor");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("LastLoginIp")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<string>("Mobile")
                        .HasMaxLength(11);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("system_user");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.UserActionLog", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("ActionName")
                        .HasMaxLength(30);

                    b.Property<string>("ActionResult")
                        .HasMaxLength(500);

                    b.Property<string>("ControllerName")
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Ip")
                        .HasMaxLength(46);

                    b.Property<string>("Operation")
                        .HasMaxLength(40);

                    b.Property<string>("Url")
                        .HasMaxLength(256);

                    b.Property<string>("UrlReferrer")
                        .HasMaxLength(256);

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("system_action_log");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.UserRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("system_user_role");
                });

            modelBuilder.Entity("Vino.Core.TimedTask.TimedTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32);

                    b.Property<bool>("AutoReset");

                    b.Property<DateTime>("BeginTime");

                    b.Property<DateTime>("ExpireTime");

                    b.Property<string>("Identifier")
                        .HasMaxLength(256);

                    b.Property<int>("Interval");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.Property<int>("RunTimes");

                    b.HasKey("Id");

                    b.ToTable("TimedTasks");
                });

            modelBuilder.Entity("Vino.Core.TimedTask.TimedTaskLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginTime");

                    b.Property<long>("Duration");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Result")
                        .HasMaxLength(20);

                    b.Property<string>("TaskId")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TimedTaskLogs");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.Membership.Member", b =>
                {
                    b.HasOne("Vino.Core.CMS.Domain.Entity.Membership.MemberType", "MemberType")
                        .WithMany()
                        .HasForeignKey("MemberTypeId");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.RoleFunction", b =>
                {
                    b.HasOne("Vino.Core.CMS.Domain.Entity.System.Function", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vino.Core.CMS.Domain.Entity.System.Role", "Role")
                        .WithMany("RoleFunctions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.UserActionLog", b =>
                {
                    b.HasOne("Vino.Core.CMS.Domain.Entity.System.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Vino.Core.CMS.Domain.Entity.System.UserRole", b =>
                {
                    b.HasOne("Vino.Core.CMS.Domain.Entity.System.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vino.Core.CMS.Domain.Entity.System.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
