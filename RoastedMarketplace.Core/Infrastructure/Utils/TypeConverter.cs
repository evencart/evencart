using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace RoastedMarketplace.Core.Infrastructure.Utils
{
    public class TypeConverter
    {
        /// <summary>
        /// Casts a property value to appropriate type
        /// </summary>
        /// <returns></returns>
        public static object CastPropertyValue(PropertyInfo property, object value)
        {
            if (property == null)
                return null;
            return CastValue(property.PropertyType, value);
        }

        public static object CastValue(Type type, object value)
        {
            if (value == null)
                return null;
            //enumeration?
            if (type.IsEnum)
            {
                var enumType = type;

                if (Enum.IsDefined(enumType, value))
                    return Enum.Parse(enumType, value.ToString());
                //check if it's int parsable
                int intValue;
                if (int.TryParse(value.ToString(), out intValue))
                {
                    if (Enum.IsDefined(enumType, intValue))
                        return Enum.Parse(enumType, value.ToString());
                }
            }
            //boolean?
            if (type == typeof(bool))
            {
                var strValue = value.ToString().ToLowerInvariant();
                return strValue == "1" || strValue == "true" || strValue == "on" || strValue == "checked";
            }

            //nullable int?
            if (type == typeof(int?))
                return Convert.ToInt32(value);

            //uri?
            if (type == typeof(Uri))
                return new Uri(value.ToString());

            //int list
            if (type == typeof(IList<int>))
            {
                if (typeof(JArray) == value.GetType())
                {
                    var jArrayValue = (JArray)value;
                    return jArrayValue.ToObject<List<int>>();
                }

            }
            //string list
            if (type == typeof(IList<string>))
            {
                if (typeof(JArray) == value.GetType())
                {
                    var jArrayValue = (JArray)value;
                    return jArrayValue.ToObject<List<string>>();
                }

            }
            //fallback
            return Convert.ChangeType(value, type);
        }
    }
}