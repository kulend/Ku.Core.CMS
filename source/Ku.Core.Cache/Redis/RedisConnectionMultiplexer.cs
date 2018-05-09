using System;
using StackExchange.Redis;
using Ku.Core.Infrastructure.Extensions;

namespace Ku.Core.Cache.Redis
{
    public class RedisConnectionMultiplexer
    {
        public static RedisConfig RedisConfig;
        //private static string RedisConnectString;

        public static void Init(RedisConfig config)
        {
            RedisConfig = config;
        }

        private readonly Lazy<ConnectionMultiplexer> _defaultConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
        {
            if (RedisConfig == null || RedisConfig.ConnectionString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(RedisConfig), "请先初始化redis配置");
            }
            return ConnectionMultiplexer.Connect(RedisConfig.ConnectionString);
        });

        public static RedisConnectionMultiplexer Instance { get; } = new RedisConnectionMultiplexer();

        public IDatabase GetDefaultDataBase()
        {
            return _defaultConnectionMultiplexer.Value.GetDatabase();
        }
    }
}
