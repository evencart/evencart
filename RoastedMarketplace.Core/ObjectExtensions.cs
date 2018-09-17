using System;
using System.Reflection;

namespace RoastedMarketplace.Core
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T entity)
        {
            //step : 1 Get the type of source object and create a new instance of that type
            var typeSource = typeof(T);
            var objTarget = Activator.CreateInstance(typeSource);

            //Step2 : Get all the properties of source object type
            var propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Step : 3 Assign all source property to taget object 's properties
            foreach (var property in propertyInfo)
            {
                //Check whether property can be written to
                if (!property.CanWrite) continue;

                //Step : 4 check whether property type is value type, enum or string type
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                {
                    property.SetValue(objTarget, property.GetValue(entity, null), null);
                }
                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                else
                {
                    var objPropertyValue = property.GetValue(entity, null);
                    property.SetValue(objTarget, objPropertyValue == null ? null : Clone(objPropertyValue), null);
                }
            }
            return (T) objTarget;
        }
    }
}