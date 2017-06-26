using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.Cache;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Core.Log;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Service;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Vino.Core.CMS.Web.Application
{
    public class VinoApp
    {
        protected IConfiguration _configuration;

        public virtual void Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this._configuration = configuration;

            //ID生成器初始化
            ID.Initialize(_configuration);
            //缓存初始化
            CacheConfig.Initialize(_configuration);
            //日志初始化
            VinoLogger.Initialize(env);
            //VinoMapper初始化
            VinoMapper.Initialize();

            //env.ConfigureNLog("nlog.config");
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connection = _configuration.GetConnectionString("MysqlDatabase");
            services.AddDbContext<VinoDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Vino.Core.CMS.Web.Admin")));
            //services.AddApplicationInsightsTelemetry(Configuration);
            // Add framework services.
            services.AddMvc();
            //加入身份认证
            services.AddAuthorization();
            //添加options
            //services.AddOptions();
            //services.Configure<RedisConfig>(Configuration.GetSection("RedisConfig"));

            //使用redis存储session
            services.AddSingleton<IDistributedCache>(
                serviceProvider =>
                    new RedisCache(new RedisCacheOptions
                    {
                        Configuration = CacheConfig.RedisConfig.ConnectionString,
                        InstanceName = CacheConfig.RedisConfig.ApplicationKey
                    }));
            services.AddSession();

            //为NLog.web注入HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var provider = IoC.InitializeWith(new DependencyResolverFactory(), services);
            return provider;
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Nlog
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");
            loggerFactory.CreateLogger("AAA").LogInformation("AAAAAAAAA");
        }
    }
}
