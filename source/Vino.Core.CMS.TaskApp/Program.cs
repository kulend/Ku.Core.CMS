using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.Cache;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.TaskApp.Application;
using Vino.Core.CMS.TaskApp.AutoMapper;

namespace Vino.Core.CMS.TaskApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 支持中文编码
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Hello World!");

            var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            //ID生成器初始化
            ID.Initialize(Configuration);
            //缓存初始化
            CacheConfig.Initialize(Configuration);

            VinoMapper.Initialize();

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            string connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<VinoDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Vino.Core.CMS.TaskApp")));

            services.AddTimedTask();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new AppModule());

            // 将原本注册在内置 DI 组件中的依赖迁移入 Autofac 中
            builder.Populate(services);

            //构建容器
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            serviceProvider.UseTimedTask();

            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd.EqualOrdinalIgnoreCase("exit"))
                {
                    break;
                }
            }
        }
    }
}