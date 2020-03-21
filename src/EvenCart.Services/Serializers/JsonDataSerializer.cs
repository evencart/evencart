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
using System.Collections.Generic;
using EvenCart.Core.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace EvenCart.Services.Serializers
{
    public class JsonDataSerializer : IDataSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter()
            },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public string Serialize(object obj, bool camelCase = true)
        {
            var currentResolver = SerializerSettings.ContractResolver;
            if (!camelCase)
                SerializerSettings.ContractResolver = new DefaultContractResolver();
            var serializedData = JsonConvert.SerializeObject(obj, Formatting.None, SerializerSettings);
            //reset the settings
            SerializerSettings.ContractResolver = currentResolver;
            return serializedData;
        }

        public T DeserializeAs<T>(string serializedData)
        {
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public object Deserialize(string serializedData, Type targetType)
        {
            return JsonConvert.DeserializeObject(serializedData, targetType);
        }
    }
}