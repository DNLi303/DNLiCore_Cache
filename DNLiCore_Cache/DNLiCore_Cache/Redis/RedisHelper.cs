using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DNLiCore_Cache_Redis
{

    public class RedisHelper : IRedisHelper
    {
        // IDistributedCache _rediscache;
        private RedisCache _rediscache = null;
        const double defaultExpiryTime = 10080;
        public RedisHelper(RedisCacheOptions options)
        {
            _rediscache = new RedisCache(options);
          
        }

        #region 设置缓存
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime">设置默认过期时间10080分钟 7天</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, double expiryTime = defaultExpiryTime)
        {
            string myValue = ConvertModelToString(value);
            return Set(key, myValue, expiryTime);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        public bool Set(string key, string value, double expiryTime = defaultExpiryTime)
        {
            try
            {
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiryTime)
                };
                _rediscache.SetString(key, value, options);
                
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        public bool Set(string key, byte[] value, double expiryTime = defaultExpiryTime)
        {
            try
            {
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiryTime)
                };
                _rediscache.Set(key, value, options);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 移除缓存
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveCache(string key)
        {
            try
            {
                _rediscache.Remove(key);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        #endregion

        #region 判断缓存是否存在
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExit(string key)
        {
            if (string.IsNullOrEmpty(Get(key)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 刷新缓存
        /// <summary>
        /// 刷新缓存(只支持string型)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Refresh(string key, double expiryTime = defaultExpiryTime)
        {
            try
            {
                string content = _rediscache.GetString(key);
                RemoveCache(key);
                Set(key, content, expiryTime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string getString = Get(key);
            if (!string.IsNullOrEmpty(getString))
            {
                T getT = ConverStringToModel<T>(getString);
                return getT;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {

            try
            {
                return _rediscache.GetString(key);
            }
            catch (Exception)
            {

                return "";
            }
        }

        /// <summary>
        /// 获取byte[]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] GetByte(string key)
        {
            try
            {
                return _rediscache.Get(key);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion


        #region 实体转字符串
        /// <summary>
        /// 实体转字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertModelToString(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, timejson);
        }
        #endregion

        #region JsonModel
        public static T ConverStringToModel<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, timejson);
        }

        public static DataTable ConverStringToDataTable(string value)
        {
            return null;
        }

        public static IsoDateTimeConverter timejson = new IsoDateTimeConverter
        {
            DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
        }; 
        #endregion
    }
}
