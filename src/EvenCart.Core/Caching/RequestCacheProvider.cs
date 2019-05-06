using System.Collections.Generic;
using EvenCart.Core.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Core.Caching
{
    public class RequestCacheProvider : FoundationCacheProvider<IDictionary<object, object>>
    {
        protected override IDictionary<object, object> InitializeCacheProvider()
        {
            return DependencyResolver.Resolve<IHttpContextAccessor>().HttpContext.Items;
        }

        public override T Get<T>(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out var obj);
            return (T)obj;
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