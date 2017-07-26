using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Vino.Core.CMS.TaskApp.Extensions
{
    public static class LogExtension
    {
        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException("services");
            services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
            return services;
        }
    }
}
