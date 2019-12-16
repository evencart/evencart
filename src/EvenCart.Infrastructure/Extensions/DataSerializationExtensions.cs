using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;

namespace EvenCart
{
    public static class DataSerializationExtensions
    {
        public static string ToJson(this object obj, bool camelCase = true)
        {
            var serializer = DependencyResolver.Resolve<IDataSerializer>();
            return serializer.Serialize(obj, camelCase);
        }

        public static T To<T>(this string jsonStr)
        {
            if (jsonStr.IsNullEmptyOrWhiteSpace())
                return default(T);
            var serializer = DependencyResolver.Resolve<IDataSerializer>();
            return serializer.DeserializeAs<T>(jsonStr);
        }
    }
}