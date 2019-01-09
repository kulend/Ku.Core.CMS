using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ku.Core.CMS.Data.Migrations.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KuDbContext>
    {

        public KuDbContext CreateDbContext(string[] args)
        {
            //Directory.SetCurrentDirectory("..");//设置当前路径为当前解决方案的路径
            //string appSettingBasePath = Directory.GetCurrentDirectory() + "/Ku.Core.CMS.Web.Backend";//appsettings.json所在的项目名称
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional:true)
                .Build();

            var builder = new DbContextOptionsBuilder<KuDbContext>();
            //builder.UseMySql(configBuilder.GetConnectionString("Mysql"), b => b.MigrationsAssembly("Ku.Core.CMS.Web.Backend"));
            builder.UseMySql(configBuilder.GetConnectionString("Mysql"));
            return new KuDbContext(builder.Options);
        }

    }
}
