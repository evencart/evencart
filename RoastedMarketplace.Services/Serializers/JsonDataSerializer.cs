using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace RoastedMarketplace.Services.Serializers
{
    public class JsonDataSerializer : IDataSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter()
            }
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