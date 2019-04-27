using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Core.Extensions
{
    public static class FoundationEntityExtensions
    {
        public static void SetMeta(this FoundationEntity entity, string key, object data)
        {
            entity.MetaData = entity.MetaData ?? new Dictionary<string, object>();
            if (entity.MetaData.ContainsKey(key))
                entity.MetaData[key] = data;
            else
            {
                entity.MetaData.Add(key, data);
            }
        }

        public static T GetMeta<T>(this FoundationEntity entity, string key)
        {
            if (entity == null || entity.MetaData == null || !entity.MetaData.ContainsKey(key))
                return default(T);
            var value = entity.MetaData[key];
            if (value.GetType() == typeof(T))
                return (T) entity.MetaData[key];
            return default(T);
        }
    }
}