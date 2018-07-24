using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ku.Core.CMS.Data.Common;
using Ku.Core.CMS.TaskApp.Application;
using Ku.Core.Infrastructure.Extensions;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Data.EntityFramework;
using Ku.Core.CMS.Domain;
using System.Reflection;

namespace Ku.Core.CMS.TaskApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 支持中文编码
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("定时任务处理程序启动!");

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
#if DEBUG
            configurationBuilder.AddJsonFile("appsettings.Development.json");
#endif
            var Configuration = configurationBuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            //AutoMapper
            services.AddAutoMapper(typeof(EntityMapperProfile).GetTypeInfo().Assembly);
            //Tools
            services.AddTools();
            //IdGenerator
            services.AddIdGenerator(Configuration);

            string connection = Configuration.GetConnectionString("MysqlDatabase");
            //services.AddDbContext<KuDbContext>(options => options.UseMySql(connection));
            services.AddDapper(options => options.UseMySql(connection));

            //缓存
            services.AddCache(Configuration);

            //事件消息发送订阅
            services.AddEventBus<KuDbContext>(Configuration);

            services.AddSingleton<TaskManager>();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new AppModule());

            // 将原本注册在内置 DI 组件中的依赖迁移入 Autofac 中
            builder.Populate(services);

            //构建容器
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            //Dapper
            serviceProvider.UseDapper();

            serviceProvider.GetService<TaskManager>().Startup();

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