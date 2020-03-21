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
using System.Linq;
using System.Net;
using EvenCart.Core.Data;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace EvenCart.Core.Caching
{
    public sealed class RedisCacheProvider : FoundationCacheProvider<IDatabase>
    {
        private readonly IDataSerializer _dataSerializer;
        private readonly IConfiguration _applicationConfiguration;
        public RedisCacheProvider(IDataSerializer dataSerializer, IConfiguration applicationConfiguration) : base(true)
        {
            _dataSerializer = dataSerializer;
            _applicationConfiguration = applicationConfiguration;
            _cache = InitializeCacheProvider();
        }

        protected override IDatabase InitializeCacheProvider()
        {
            return GetConnection().GetDatabase();
        }

        public override T Get<T>(string cacheKey)
        {
            return (T) Get(cacheKey, typeof(T));
        }

        public override object Get(string cacheKey, Type type)
        {
            var serialized = _cache.StringGet(cacheKey);
            return _dataSerializer.Deserialize(serialized, type);
        }

        public override bool IsSet(string cacheKey)
        {
            return _cache.KeyExists(cacheKey);
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
            var serialized = _dataSerializer.Serialize(cacheData);
            _cache.StringSet(cacheKey, serialized, TimeSpan.FromMinutes(expiration));
        }

        public override void Remove(string cacheKey)
        {
            _cache.KeyDelete(cacheKey);
        }

        public override void Clear()
        {
            var cache = _cache;
            var endPoints = cache.Multiplexer.GetEndPoints();
            
            foreach (var endpoint in endPoints)
            {
                var keys = GetEndpointKeys(endpoint, cache);
                foreach (var key in keys)
                {
                    cache.KeyDelete(key);
                }
            }
        }

        #region Helpers

        private ConnectionMultiplexer _connectionMultiplexer;
        private ConnectionMultiplexer GetConnection()
        {
            var connectionString = _applicationConfiguration["redisConfig"];
            if (_connectionMultiplexer == null || !_connectionMultiplexer.IsConnected)
            {
                var oldCm = _connectionMultiplexer;
                _connectionMultiplexer = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(connectionString));
                oldCm?.Dispose();
            }
            return _connectionMultiplexer;
        }

        private RedisKey[] GetEndpointKeys(EndPoint endpoint, IDatabase db)
        {
            var server = db.Multiplexer.GetServer(endpoint);
            return server.Keys().ToArray();
        }

        #endregion
        
    }
}