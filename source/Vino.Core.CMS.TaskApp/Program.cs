using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.TaskApp.Application;
using Vino.Core.CMS.TaskApp.AutoMapper;
using Vino.Core.Infrastructure.Extensions;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Vino.Core.CMS.IService.System;

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

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            //AutoMapper
            services.AddAutoMapper();
            //Tools
            services.AddTools();
            //IdGenerator
            services.AddIdGenerator(Configuration);

            string connection = Configuration.GetConnectionString("MysqlDatabase");
            services.AddDbContext<VinoDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Vino.Core.CMS.TaskApp")));

            //string connection = Configuration.GetConnectionString("Default");
            //services.AddDbContext<VinoDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Vino.Core.CMS.TaskApp")), ServiceLifetime.Transient);

            //缓存
            services.AddCache(Configuration);

            //事件消息发送订阅
            services.AddEventBus<VinoDbContext>(Configuration);

            services.AddTimedTask().AddEntityFrameworkTimedTask<VinoDbContext>();

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