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
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheProvider cacheProvider, string cacheKey, Func<T> retrieveFunc, int expiration = 60)
        {
            return (T) Get(cacheProvider, cacheKey, typeof(T), () => retrieveFunc(), expiration);
        }

        public static object Get(this ICacheProvider cacheProvider, string cacheKey, Type type, Func<object> retrieveFunc, int expiration = 60)
        {
            //check if it's available in memory cache
            if (CacheProviders.RequestProvider != null && CacheProviders.RequestProvider.IsSet(cacheKey))
                return CacheProviders.RequestProvider.Get(cacheKey, type);

            if (cacheProvider.IsSet(cacheKey))
                return cacheProvider.Get(cacheKey, type);

            //retrive the value
            var tValue = retrieveFunc();

            //set this value
            cacheProvider.Set(cacheKey, tValue, expiration);
            if (CacheProviders.RequestProvider != null)
                //save the same value in request cache if it's again requested in the same request
                CacheProviders.RequestProvider.Set(cacheKey, tValue, int.MaxValue);
            return tValue;
        }
    }
}