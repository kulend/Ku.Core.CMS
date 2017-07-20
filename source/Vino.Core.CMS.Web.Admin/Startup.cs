using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vino.Core.CMS.Web.Application;

namespace Vino.Core.CMS.Web.Admin
{
    public class Startup
    {
        public BackendAppStartup vinoApp;

        public Startup(IHostingEnvironment env)
        {
            //应用相关初始化
            vinoApp = new BackendAppStartup(env);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return vinoApp.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            vinoApp.Configure(app, env, loggerFactory);
        }
    }
}
