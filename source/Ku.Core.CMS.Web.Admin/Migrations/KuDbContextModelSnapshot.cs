using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ku.Core.CMS.Data.Common;

namespace Ku.Core.CMS.Web.Admin.Migrations
{
    [DbContext(typeof(KuDbContext))]
    partial class KuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Ku.Core.CMS.Data.Entity.System.Operator", b =>
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

                    b.Property<string>("OperatorName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("system_operators");
                });
        }
    }
}
