namespace RoastedMarketplace.Services.Serializers
{
    public interface IDataSerializer
    {
        string Serialize(object obj, bool camelCase = true);

        T DeserializeAs<T>(string serializedData);
    }
}