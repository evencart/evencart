using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RoastedMarketplace.Services.Serializers
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public T DeserializeAs<T>(string serializedData)
        {
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }
}