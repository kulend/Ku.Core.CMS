using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Vino.Core.Cache.Redis
{
    public class RedisConnectionMultiplexer
    {
        private static readonly string RedisConnectString = CacheConfig.RedisConfig.ConnectionString;
        private readonly Lazy<ConnectionMultiplexer> _defaultConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectString));

        public static RedisConnectionMultiplexer Instance { get; } = new RedisConnectionMultiplexer();

        public IDatabase GetDefaultDataBase()
        {
            return _defaultConnectionMultiplexer.Value.GetDatabase();
        }
    }
}
