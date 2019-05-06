using System;

namespace EvenCart.Services.Serializers
{
    public interface IDataSerializer
    {
        string Serialize(object obj, bool camelCase = true);

        T DeserializeAs<T>(string serializedData);

        object Deserialize(string serializedData, Type targetType);
    }
}