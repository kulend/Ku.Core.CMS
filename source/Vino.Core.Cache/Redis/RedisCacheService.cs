using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ku.Core.Cache.Redis
{
    public class RedisCacheService: ICacheService
    {
        private string GetKey(string key)
        {
            return $"{RedisConnectionMultiplexer.RedisConfig.ApplicationKey ?? ""}.{key}";
        }

        public void Add(string key, object value, TimeSpan time)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();
            if (value == null)
            {
                //认定为删除该键
                this.Remove(key);
                return;
            }
            var newTime = time == TimeSpan.Zero ? (TimeSpan?)null : time;
            database.StringSet(GetKey(key), JsonConvert.SerializeObject(value), newTime);
        }

        public void Add(string key, object value)
        {
            this.Add(key, value, TimeSpan.Zero);
        }

        public T Get<T>(string key)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();
            var result = database.StringGet(GetKey(key));
            if (result.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            return default(T);
        }

        public bool IsSet(string key)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();

            return database.KeyExists(GetKey(key));
        }

        public void Remove(string key)
        {
            RedisConnectionMultiplexer.Instance.GetDefaultDataBase().KeyDelete(GetKey(key));
        }

        public void RemoveByPattern(string pattern)
        {
            RedisConnectionMultiplexer.Instance.GetDefaultDataBase().KeyDeleteWithPrefix(GetKey(pattern));
        }

        public void Clear()
        {
            RedisConnectionMultiplexer.Instance.GetDefaultDataBase().KeyDeleteWithPrefix(GetKey("*"));
        }
    }
}
