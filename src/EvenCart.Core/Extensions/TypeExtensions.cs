using System;
using System.Linq;

namespace EvenCart.Core.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Type[] PrimitiveTypes = {
            typeof(Enum),
            typeof(String),
            typeof(Char),
            typeof(Guid),

            typeof(Boolean),
            typeof(Byte),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),

            typeof(SByte),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),

            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),

        };
        public static bool IsPrimitive(this Type type)
        {
            return
                type.IsPrimitive || type.IsEnum || PrimitiveTypes.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 IsPrimitive(type.GetGenericArguments()[0]));
        }
    }
}