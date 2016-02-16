using System;

//namespace OmarALZabir.AspectF
using System.Runtime.Caching;

namespace Utility.通用
{
    public interface ICache
    {
        void Add(string key, object value);
        void Add(string key, object value, TimeSpan timeout);
        void Set(string key, object value);
        void Set(string key, object value, TimeSpan timeout);
        bool Contains(string key);
        object Get(string key);
        void Remove(string key);
    }

    class Cache : ICache
    {
        private readonly ObjectCache cache = MemoryCache.Default;

        public void Add(string key, object value)
        {
            cache.Add(key, value, DateTimeOffset.MaxValue);
        }

        public void Add(string key, object value, TimeSpan timeout)
        {
            cache.Add(key, value, new DateTimeOffset(DateTime.Now, timeout));
        }

        public void Set(string key, object value)
        {
            cache.Set(key, value, DateTimeOffset.MaxValue);
        }

        public void Set(string key, object value, TimeSpan timeout)
        {
            cache.Set(key, value, new DateTimeOffset(DateTime.Now, timeout));
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }

}
