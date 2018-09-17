using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace RoastedMarketplace.Core.Caching
{
    public class HttpCacheProvider : FoundationCacheProvider<Cache>
    {

        protected override Cache InitializeCacheProvider()
        {
            return HttpRuntime.Cache;
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
            return _cache[cacheKey] != null;
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
            _cache.Insert(cacheKey, cacheData, null, DateTime.Now.AddMinutes(expiration), TimeSpan.Zero);
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