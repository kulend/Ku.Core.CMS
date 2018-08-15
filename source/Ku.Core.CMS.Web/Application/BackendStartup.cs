using Ku.Core.CMS.Data.EntityFramework;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.Infrastructure.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Ku.Core.CMS.Web.Application
{
    public class BackendStartup : BaseStartup
    {
        public BackendStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Dapper
            string connection = Configuration.GetConnectionString("Mysql");
            services.AddDapper(options => options.UseMySql(connection));
            services.AddDbContextPool<KuDbContext>(options => options.UseMySql(connection, b => b.MigrationsAssembly("Ku.Core.CMS.Web.Backend")));

            //微信
            services.AddWeChat();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session
            services.AddSession(Configuration);

            //JWT
            services.AddJwtToken(Configuration);
            services.AddBackendAuth(Configuration, Environment);

            //使用Layui
            services.AddLayui(opt => {
                opt.ActionsInFormItem = false;
                opt.ActionTagTheme = "layui-btn-primary";
                opt.ActionTagSize = "layui-btn-sm";
            });

            services.AddMvc(opts =>
                {
                    opts.Filters.Add(typeof(VinoActionFilter));
                    opts.Filters.Add(typeof(PageLockFilter));
                    opts.Filters.Add(typeof(JsonWrapperAsyncResultFilter));
                    opts.Filters.Add(typeof(JsonWrapperResultFilter));
                    opts.Filters.Add(typeof(ExceptionFilter));
                }).AddJsonOptions(json =>
                {
                    // 忽略循环引用
                    json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    json.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    json.SerializerSettings.Converters.Add(new LongToStringConverter());
                    json.SerializerSettings.Converters.Add(new EnumDisplayConverter());
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseSession(Configuration);

            //身份认证
            app.UseBackendAuth(Configuration);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login",
                    template: "Login",
                    defaults: new { area = "Default", controller = "Home", action = "Login" });

                routes.MapRoute(
                    name: "accessDenied",
                    template: "AccessDenied",
                    defaults: new { area = "Default", controller = "Home", action = "AccessDenied" });

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
