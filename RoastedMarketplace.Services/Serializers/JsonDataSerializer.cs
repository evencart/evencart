using Newtonsoft.Json;

namespace RoastedMarketplace.Services.Serializers
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T DeserializeAs<T>(string serializedData)
        {
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }
}