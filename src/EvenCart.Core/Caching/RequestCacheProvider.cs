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