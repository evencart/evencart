using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.EntityProperties;
using RoastedMarketplace.Data.Interfaces;
using RoastedMarketplace.Services.EntityProperties;
using Newtonsoft.Json;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Services.Extensions
{
    public static class EntityPropertyExtensions
    {
        /// <summary>
        /// Gets the properties of entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IList<EntityProperty> GetProperties<T>(this IHasEntityProperties<T> entity) where T: FoundationEntity
        {
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            return entityPropertyService.Get(x => x.EntityName == typeof(T).Name && x.EntityId == entity.Id).ToList();
        }
        /// <summary>
        /// Gets the property with specified name for current entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static EntityProperty GetProperty<T>(this IHasEntityProperties<T> entity, string propertyName) where T : FoundationEntity
        {
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            return
                entityPropertyService.Get(
                    x => x.EntityName == typeof(T).Name && x.EntityId == entity.Id && x.PropertyName == propertyName).FirstOrDefault();
        }

        /// <summary>
        /// Gets the property valueas stored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this IHasEntityProperties<T> entity, string propertyName) where T : FoundationEntity
        {
            var entityProperty = GetProperty(entity, propertyName);
            return entityProperty?.Value;
        }

        /// <summary>
        /// Gets property value as the target type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetPropertyValueAs<T>(this IHasEntityProperties entity, string propertyName, T defaultValue = default(T))
        {
            if (entity == null)
                return defaultValue;

            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            var typeName = entity.GetType().BaseType?.Name ?? entity.GetType().Name;
            var entityProperty =  entityPropertyService.Get(
                    x => x.EntityName == typeName && x.EntityId == entity.Id && x.PropertyName == propertyName).FirstOrDefault();

            if (entityProperty == null)
                return defaultValue;

            return JsonConvert.DeserializeAnonymousType(entityProperty.Value, defaultValue);
        }

        public static void SetPropertyValue<T>(this IHasEntityProperties<T> entity, string propertyName, object value)
            where T : FoundationEntity
        {
            //does this property exist?
            var property = GetProperty(entity, propertyName) ?? new EntityProperty()
            {
                EntityId = entity.Id,
                EntityName = typeof(T).Name,
                PropertyName = propertyName
            };
            

            property.Value = JsonConvert.SerializeObject(value);
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            if (property.Id == 0)
                entityPropertyService.Insert(property);
            else
                entityPropertyService.Update(property);
        }
    }
}