using System;
using System.Web;
using System.Web.Caching;

namespace HT.Utility
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class XCache : ICache
    {
        private readonly Cache _cache = HttpRuntime.Cache;
        public void Add(string key, object value)
        {
            _cache.Insert(key, value, null, DateTime.UtcNow.AddMinutes(20), TimeSpan.Zero);
        }

        public void Add(string key, object value, int expire)
        {
            _cache.Insert(key, value, null, DateTime.UtcNow.AddMinutes(expire), TimeSpan.Zero);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }
    }
}