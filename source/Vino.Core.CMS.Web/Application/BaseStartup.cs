using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Vino.Core.CMS.Data.Common;

namespace Vino.Core.CMS.Web.Application
{
    public class BaseStartup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public BaseStartup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //AutoMapper
            services.AddAutoMapper();
            //Tools
            services.AddTools();
            //IdGenerator
            services.AddIdGenerator(Configuration);
            //DBContext
            string connection = Configuration.GetConnectionString("MysqlDatabase");
            services.AddDbContext<VinoDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Vino.Core.CMS.Web.Backend")));
            services.AddDapper(options => options.UseMySql(connection));

            //缓存
            services.AddCache(Configuration);

            //事件消息发送订阅
            services.AddEventBus<VinoDbContext>(Configuration);

            //微信
            services.AddWeChat();

            //Autofac依赖注入
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AppModule());
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //事件消息发送订阅
            app.UseEventBus();
        }
    }
}
