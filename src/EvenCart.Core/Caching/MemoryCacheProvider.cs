using System;
using Microsoft.Extensions.Caching.Memory;

namespace EvenCart.Core.Caching
{
    public class MemoryCacheProvider : FoundationCacheProvider<MemoryCache>
    {
        protected override MemoryCache InitializeCacheProvider()
        {
            return _CreateCacheObject();
        }

        public override T Get<T>(string cacheKey)
        {
            return (T) _cache.Get(cacheKey);
        }

        public override bool IsSet(string cacheKey)
        {
            return _cache.Get(cacheKey) != null;
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
           _cache.Set(cacheKey, cacheData, DateTimeOffset.Now.AddMinutes(expiration));
        }

        public override void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public override void Clear()
        {
           //dispose existing cache
           var existingCache = _cache;
           _cache = _CreateCacheObject();
           existingCache.Dispose();
        }

        #region Helpers
        private static MemoryCache _CreateCacheObject()
        {
            return new MemoryCache(new MemoryCacheOptions());
        } 
        #endregion
    }
}