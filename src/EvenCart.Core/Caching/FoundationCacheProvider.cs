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
    public abstract class FoundationCacheProvider<TCache> : ICacheProvider
    {
        protected TCache _cache;

        protected FoundationCacheProvider()
        {
            _cache = InitializeCacheProvider();
        }

        protected abstract TCache InitializeCacheProvider();

        public virtual void Dispose()
        {
            GC.Collect();
            //do nothing..children can use if they want to do anything
        }

        public abstract T Get<T>(string cacheKey);

        public abstract bool IsSet(string cacheKey);

        public abstract void Set<T>(string cacheKey, T cacheData, int expiration = 60);

        public abstract void Remove(string cacheKey);

        public abstract void Clear();
    }
}