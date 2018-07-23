using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Data.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KuDbContext>
    {

        public KuDbContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile("appsettings.Development.json", optional: true)
            //    .Build();

            var builder = new DbContextOptionsBuilder<KuDbContext>();
            builder.UseMySql("server=121.40.195.153;userid=ku.core.cms;pwd=7cd9b936ddace67f;port=5306;database=ku.core.cms;sslmode=none;", b => b.MigrationsAssembly("Ku.Core.CMS.Web.Backend"));
            return new KuDbContext(builder.Options);
        }

    }
}
