using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSession(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Session");
            var type = section["Type"] ?? "default";

            if (type.Equals("redis", StringComparison.CurrentCultureIgnoreCase))
            {
                //使用redis存储session
                services.AddSingleton<IDistributedCache>(
                    serviceProvider =>
                        new RedisCache(new RedisCacheOptions
                        {
                            Configuration = configuration["Session:Redis:ConnectionString"],
                            InstanceName = configuration["Session:Redis:ApplicationKey"]
                        }));
            }

            services.AddSession();
            return services;
        }

    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class SessionBuilderExtensions
    {
        public static IApplicationBuilder UseSession(this IApplicationBuilder app, IConfiguration configuration)
        {
            var timeout = 30;
            if (!string.IsNullOrEmpty(configuration["Session:Timeout"]))
            {
                int.TryParse(configuration["Session:Timeout"], out timeout);
            }
            app.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(timeout) });
            return app;
        }
    }
}
