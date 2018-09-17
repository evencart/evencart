using System;

namespace RoastedMarketplace.Core.Caching
{
    public interface ICacheProvider : IDisposable
    {
        /// <summary>
        /// Gets the cached item with specified cache key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        T Get<T>(string cacheKey);

        /// <summary>
        /// Checks if the particular cache key is set in the cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        bool IsSet(string cacheKey);

        /// <summary>
        /// Sets the item in the cache with specified cache key and data
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheData"></param>
        /// <param name="expiration"></param>
        void Set<T>(string cacheKey, T cacheData, int expiration = 60);

        /// <summary>
        /// Removes the specified cache key from the cache
        /// </summary>
        /// <param name="cacheKey"></param>
        void Remove(string cacheKey);

        /// <summary>
        /// Clears the cache
        /// </summary>
        void Clear();
    }
}