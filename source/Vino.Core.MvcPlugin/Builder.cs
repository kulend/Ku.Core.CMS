using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.MvcPlugin
{
    public static class Builder
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }

        public static IPluginLoader AddMvcPlugin(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Builder.ServiceCollection = services;
            Builder.Configuration = configuration;

            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<RazorViewEngineOptions>, PluginRazorViewEngineOptionsSetup>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IActionDescriptorProvider, ActionDescriptorProvider>());
            services.TryAddSingleton<IPluginLoader, PluginLoader>();

            services.AddTransient<IPluginLoader, PluginLoader>();

            return new PluginLoader(hostingEnvironment);
        }

    }
}
