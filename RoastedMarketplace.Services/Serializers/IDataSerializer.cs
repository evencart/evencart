namespace RoastedMarketplace.Services.Serializers
{
    public interface IDataSerializer
    {
        string Serialize(object obj);

        T DeserializeAs<T>(string serializedData);
    }
}