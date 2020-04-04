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
using System.Text;
using EvenCart.Core;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Infrastructure.DataTransfer
{
    public class JsonProvider : IDataTransferProvider
    {
        private readonly IDataSerializer _dataSerializer;
        public JsonProvider(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }

        public DataTransferChunk GetTransferChunks(IList<Product> products)
        {
            return new DataTransferChunk()
            {
                Bytes = GetJsonBytes(products)
            };
        }

        public DataTransferChunk GetTransferChunks(IList<Category> categories)
        {
            return new DataTransferChunk()
            {
                Bytes = GetJsonBytes(categories)
            };
        }

        public DataTransferChunk GetTransferChunks(IList<User> users)
        {
            return new DataTransferChunk()
            {
                Bytes = GetJsonBytes(users)
            };
        }

        public IList<Product> GetProducts(DataTransferChunk chunk)
        {
            var json = GetJsonObject(chunk.Bytes);
            return (IList<Product>)json;
        }

        public IList<Category> GetCategories(DataTransferChunk chunk)
        {
            var json = GetJsonObject(chunk.Bytes);
            return (IList<Category>)json;
        }

        public IList<User> GetUsers(DataTransferChunk chunk)
        {
            var json = GetJsonObject(chunk.Bytes);
            return (IList<User>) json;
        }

        private byte[] GetJsonBytes(object obj)
        {
            var json = _dataSerializer.Serialize(new
            {
                version = AppVersionEvaluator.Version,
                Data = obj
            });
            var jsonBytes = Encoding.UTF8.GetBytes(json);
            return jsonBytes;
        }

        private object GetJsonObject(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            var deserialized = _dataSerializer.DeserializeAs<dynamic>(json);
            return deserialized.Data;
        }
    }
}