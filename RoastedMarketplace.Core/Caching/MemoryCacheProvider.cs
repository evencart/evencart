// #region Author Information
// // MemoryCacheProvider.cs
// // 
// // (c) 2017 Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace RoastedMarketplace.Core.Caching
{
    public class MemoryCacheProvider : FoundationCacheProvider<IMemory>
    {
        protected override MemoryCache InitializeCacheProvider()
        {
            return MemoryCache.Default;
        }

        public override void Dispose()
        {
           
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
            foreach (KeyValuePair<string, object> item in _cache)
            {
                Remove(item.Key);
            }
        }
    }
}