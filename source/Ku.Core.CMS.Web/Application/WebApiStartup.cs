using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.Infrastructure.Json;
using Dnc.Api.Throttle;
using Ku.Core.CMS.Web.Extensions;
using Dnc.Api.Throttle.Redis;

namespace Ku.Core.CMS.Web.Application
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

            //ApiRestriction
            services.AddApiThrottle(options => {
                options.RedisConnectionString = "121.40.195.153:7480,password=ku123456,connectTimeout=5000,allowAdmin=false,defaultDatabase=0";
                options.OnUserIdentity = httpContext => httpContext.User.GetUserIdOrZero().ToString();
            });

            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(ApiThrottleActionFilter));
                opts.Filters.Add(typeof(VinoActionFilter));
                opts.Filters.Add(typeof(WebApiResultFilter));
                opts.Filters.Add(typeof(ApiExceptionFilter));
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

            //版本控制
            services.AddApiVersioning(option => {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"), new QueryStringApiVersionReader("api-version"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Ku.Core.CMS 接口文档",
                    Description = "RESTful API for Vino.Core.CMS",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Kulend", Email = "kulend@qq.com", Url = "" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Ku.Core.CMS.WebApi.xml");
                c.IncludeXmlComments(xmlPath);

                //  c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
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

            app.UseHttpsRedirection();

            app.UseWebApiAuth();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
