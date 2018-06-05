using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.Cache
{
    public interface ICacheProvider
    {
        void Set(string key, object value, TimeSpan? expiration = null);

        Task SetAsync(string key, object value, TimeSpan? time = null);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="key">Key</param>
        /// <returns>返回指定的缓存对象</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="key">Key</param>
        /// <returns>返回指定的缓存对象</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 指定的Key是否有缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        bool Exists(string key);

        /// <summary>
        /// 指定的Key是否有缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 删除指定key的缓存
        /// </summary>
        /// <param name="cacheKey">key</param>
        void Remove(string cacheKey);

        /// <summary>
        /// 删除指定key的缓存
        /// </summary>
        /// <param name="cacheKey">key</param>
        void Remove(params string[] cacheKeys);

        Task RemoveAsync(string cacheKey);

        Task RemoveAsync(params string[] cacheKeys);

        void RemoveByPrefix(string prefix);

        Task RemoveByPrefixAsync(string prefix);
    }
}
