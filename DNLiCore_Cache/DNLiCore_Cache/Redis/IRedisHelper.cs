using System;
using System.Collections.Generic;
using System.Text;

namespace DNLiCore_Cache.Redis
{
    public interface IRedisHelper
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, double expiryTime = 10080);
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        bool Set(string key, string value, double expiryTime = 10080);
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        bool Set(string key, byte[] value, double expiryTime = 10080);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveCache(string key);
        /// <summary>
        /// 是否存在key值的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExit(string key);

        /// <summary>
        /// 刷新当前缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Refresh(string key, double expiryTime = 10080);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);
        /// <summary>
        /// 获取byte[]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] GetByte(string key);
    }
}
