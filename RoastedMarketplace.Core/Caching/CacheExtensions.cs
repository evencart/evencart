using System;

namespace RoastedMarketplace.Core.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheProvider cacheProvider, string cacheKey, Func<T> retrieveFunc, int expiration = 60)
        {
            if (cacheProvider.IsSet(cacheKey))
                return cacheProvider.Get<T>(cacheKey);

            //retrive the value
            var tValue = retrieveFunc();

            //set this value
            cacheProvider.Set(cacheKey, tValue, expiration);
            return tValue;
        }
    }
}