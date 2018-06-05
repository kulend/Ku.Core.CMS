using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Ku.Core.Cache;
using Ku.Core.Cache.Redis;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                services.Configure<RedisOptions>(configuration.GetSection("Cache:Redis"));
                services.TryAddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
                services.TryAddSingleton<ICacheProvider, RedisCacheProvider>();
            }
            else
            {
                throw new ArgumentNullException("不存在此缓存插件");
            }

            return services;
        }
    }
}
