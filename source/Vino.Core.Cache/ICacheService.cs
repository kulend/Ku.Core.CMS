using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Cache
{
    public interface ICacheService
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="time">相对DateTime.Now后累加的时间</param>
        void Add(string key, object value, TimeSpan time);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        void Add(string key, object value);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="key">Key</param>
        /// <returns>返回指定的缓存对象</returns>
        T Get<T>(string key);
        /// <summary>
        /// 指定的Key是否有缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        bool IsSet(string key);
        /// <summary>
        /// 删除指定key的缓存
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);
        /// <summary>
        /// 根据匹配表达式删除
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        void RemoveByPattern(string pattern);
        /// <summary>
        /// 清空所有的缓存
        /// </summary>
        void Clear();
    }
}
