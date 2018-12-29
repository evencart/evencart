using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RoastedMarketplace.Services.Serializers
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize(object obj, bool camelCase = true)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = camelCase ? new CamelCasePropertyNamesContractResolver() : new DefaultContractResolver()
            });
        }

        public T DeserializeAs<T>(string serializedData)
        {
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }
}