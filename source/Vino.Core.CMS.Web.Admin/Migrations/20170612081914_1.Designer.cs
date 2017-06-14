using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Vino.Core.CMS.Data.Common;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    [DbContext(typeof(VinoDbContext))]
    [Migration("20170612081914_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.Menu", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Icon")
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("OrderIndex");

                    b.Property<long>("ParentId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.Property<short>("Type");

                    b.Property<string>("Url")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("system_menu");
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.Role", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("system_role");
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.RoleMenu", b =>
                {
                    b.Property<long>("RoleId");

                    b.Property<long>("MenuId");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("system_role_menu");
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.User", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateTime");

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
                        .HasMaxLength(20);

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("system_user");
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.UserRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("system_user_role");
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.RoleMenu", b =>
                {
                    b.HasOne("Vino.Core.CMS.Data.Entity.System.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vino.Core.CMS.Data.Entity.System.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vino.Core.CMS.Data.Entity.System.UserRole", b =>
                {
                    b.HasOne("Vino.Core.CMS.Data.Entity.System.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vino.Core.CMS.Data.Entity.System.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
