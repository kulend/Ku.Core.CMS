using Dnc.Api.Throttle;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Ku.Core.CMS.Web.Application
{
    public class PcSiteStartup : BaseStartup
    {
        public PcSiteStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Dapper
            string connection = Configuration.GetConnectionString("Mysql");
            services.AddDapper(options => options.UseMySql(connection));

            //HttpClient
            services.AddHttpClient();
            services.AddHttpClient("qq_login", c => {
                    c.BaseAddress = new Uri("https://graph.qq.com/");
            });

            //Api限流
            services.AddApiThrottle(options => {
                options.UseRedisCacheAndStorage(redisOption => {
                    redisOption.ConnectionString = "121.40.195.153:7480,password=ku123456,connectTimeout=5000,allowAdmin=false,defaultDatabase=0";
                });
                //options.OnUserIdentity = httpContext => httpContext.User.GetUserIdOrZero().ToString();
                options.onIntercepted = (context, valve, where) =>
                {
                    if (where == IntercepteWhere.Middleware)
                    {
                        return new JsonResult(new { code = 906, message = "访问过于频繁，请稍后重试！" });
                    }
                    else
                    {
                        return new OriginJsonResult(new { code = 906, message = "访问过于频繁，请稍后重试！" });
                    }
                };
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString(Configuration["Authentication:LoginPath"]);
                    o.AccessDeniedPath = new PathString(Configuration["Authentication:AccessDeniedPath"]);
                    o.Cookie.Name = Configuration["Authentication:CookieName"];
                    o.Cookie.HttpOnly = true;
                });

            services.AddSession();

            services.AddCors();

            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(ApiThrottleActionFilter));
                opts.Filters.Add(typeof(PageViewRecordFilter));
                opts.Filters.Add(typeof(JsonWrapperAsyncResultFilter));
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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePages(async context =>
            {
                if (!context.HttpContext.Request.IsJsonRequest())
                {
                    context.HttpContext.Response.Redirect("/Error/" + context.HttpContext.Response.StatusCode);
                }
            });

            //app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseStaticFiles();

            app.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(30) });

            app.UseAuthentication();

            app.UseCors(builder => builder
                .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                );

            app.UseMvc();
        }
    }
}
