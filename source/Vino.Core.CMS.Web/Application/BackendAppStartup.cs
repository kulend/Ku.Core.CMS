using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Vino.Core.CMS.Web.Application
{
    public class BackendAppStartup: VinoAppStartup
    {
        public BackendAppStartup(IHostingEnvironment env) : base(env)
        {

        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddJwtToken(Configuration);
            services.AddJwtAuth(Configuration);

            services.TryAddTransient(typeof(IHtmlHelper<>), typeof(VinoHtmlHelper<>));

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
            }

            app.UseStaticFiles();

            app.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(30) });

            //身份认证
            app.UseJwtAuth(Configuration);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login",
                    template: "Login",
                    defaults: new { controller = "Home", action = "Login" });

                routes.MapRoute(
                    name: "accessDenied",
                    template: "AccessDenied",
                    defaults: new { controller = "Home", action = "AccessDenied" });

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
