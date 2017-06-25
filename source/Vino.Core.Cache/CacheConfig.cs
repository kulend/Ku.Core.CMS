using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.Cache.Redis;

namespace Vino.Core.Cache
{
    public static class CacheConfig
    {
        public static RedisConfig RedisConfig;

        public static void Initialize(IConfiguration configuration)
        {
            RedisConfig = new RedisConfig();
            var section = configuration.GetSection("RedisConfig");
            if (section == null)
            {
                RedisConfig.ConnectionString = "";
                RedisConfig.ApplicationKey = "";
            }
            else
            {
                //从配置文件中获取
                var connectionString = section["ConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = "localhost";
                }
                RedisConfig.ConnectionString = connectionString;
                var applicationKey = section["ApplicationKey"];
                RedisConfig.ApplicationKey = applicationKey;
            }
        }
    }
}
