using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Vino.Core.Cache;
using Vino.Core.Cache.Redis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Cache");
            var type = section["Type"] ?? "redis";

            if (type.Equals("redis", StringComparison.CurrentCultureIgnoreCase))
            {
                var config = new RedisConfig
                {
                    ConnectionString = configuration["Cache:Redis:ConnectionString"],
                    ApplicationKey = configuration["Cache:Redis:ApplicationKey"]
                };
                RedisConnectionMultiplexer.Init(config);

                services.AddSingleton(typeof(ICacheService), typeof(RedisCacheService));
            }
            else
            {
                throw new ArgumentNullException("不存在此缓存插件");
            }

            return services;
        }
    }
}
