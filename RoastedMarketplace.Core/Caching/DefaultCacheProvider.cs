using System;
using System.Collections.Generic;

namespace RoastedMarketplace.Core.Caching
{
    public class DefaultCacheProvider : FoundationCacheProvider<Dictionary<string, object>>
    {
        protected override Dictionary<string, object> InitializeCacheProvider()
        {
            return new Dictionary<string, object>();
        }

        public override void Dispose()
        {
            GC.Collect();
        }

        public override T Get<T>(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out object obj);
            return (T) obj;
        }

        public override bool IsSet(string cacheKey)
        {
            return _cache.ContainsKey(cacheKey);
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
            _cache.TryAdd(cacheKey, cacheData);
        }

        public override void Remove(string cacheKey)
        {
            _cache[cacheKey] = null;
        }

        public override void Clear()
        {
            _cache.Clear();
        }
    }
}