using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ku.Core.CMS.WinService
{
    public class Startup : WinServiceStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {

        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TaskManager>();

            return base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);

            app.ApplicationServices.GetService<TaskManager>().Startup();
        }
    }
}
