using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Vino.Core.Cache;
using Vino.Core.Cache.Redis;
using Vino.Core.CMS.Service.Events;
using Vino.Core.EventBus;
using Vino.Core.EventBus.CAP;

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

                //使用redis存储session
                services.AddSingleton<IDistributedCache>(
                    serviceProvider =>
                        new RedisCache(new RedisCacheOptions
                        {
                            Configuration = config.ConnectionString,
                            InstanceName = config.ApplicationKey
                        }));
                services.AddSession();
            }
            else
            {
                throw new ArgumentNullException("不存在此缓存插件");
            }

            return services;
        }
    }
}
