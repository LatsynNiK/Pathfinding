using System;
using System.Runtime.Caching;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class CacheService : ICacheService
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public T Get<T>(Guid id, Func<T> firstGetFunc) where T : BaseSmModel
        {
            var key = typeof(T)+":"+ id;
            var result = (T)Cache.Get(key);
            if (result == null)
            {
                result = firstGetFunc.Invoke();
                Cache.Set(key, result, new CacheItemPolicy());
            }

            return result;
        }
    }
}
