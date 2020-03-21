#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;

namespace EvenCart.Core.Caching
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
        /// Gets the cached item with specified cache key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        object Get(string cacheKey, Type type);

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