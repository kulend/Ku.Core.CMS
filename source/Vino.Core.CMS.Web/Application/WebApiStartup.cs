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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TwBusManagement接口文档",
                    Description = "RESTful API for TwBusManagement",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Alvin_Su", Email = "asdasdasd@outlook.com", Url = "" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Vino.Core.CMS.WebApi.xml");
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
