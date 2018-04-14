using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Web.Filters;
using Vino.Core.Infrastructure.Json;

namespace Vino.Core.CMS.Web.Application
{
    public class WebApiStartup : BaseStartup
    {
        public WebApiStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //JWT
            services.AddJwtToken(Configuration);
            services.AddWebApiAuth(Configuration, Environment);

            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(VinoActionFilter));
                opts.Filters.Add(typeof(WebApiResultFilter));
                opts.Filters.Add(typeof(ApiExceptionFilter));
                //opts.Filters.Add(typeof(UserActionLogFilter));
            })
                .AddJsonOptions(json =>
                {
                    // 忽略循环引用
                    json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    json.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    json.SerializerSettings.Converters.Add(new LongToStringConverter());
                    json.SerializerSettings.Converters.Add(new EnumDisplayConverter());
                });

            //版本控制
            services.AddApiVersioning(option => {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"), new QueryStringApiVersionReader("api-version"));
            });

            return base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebApiAuth();

            app.UseMvc();
        }
    }
}
