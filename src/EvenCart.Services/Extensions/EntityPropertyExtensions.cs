#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.EntityProperties;
using EvenCart.Data.Interfaces;
using EvenCart.Services.EntityProperties;
using Newtonsoft.Json;
using EvenCart.Core.Infrastructure;

namespace EvenCart.Services.Extensions
{
    public static class EntityPropertyExtensions
    {
        /// <summary>
        /// Gets the properties of entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IList<EntityProperty> GetProperties(this IHasEntityProperties entity)
        {
            var typeName = entity.GetType().Name;
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            //get all in one go for performance reasons
            entity.EntityProperties = entity.EntityProperties ?? entityPropertyService.Get(
                                          x => x.EntityName == typeName && x.EntityId == entity.Id).ToList();
            return entity.EntityProperties;
        }
        /// <summary>
        /// Gets the property with specified name for current entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static EntityProperty GetProperty(this IHasEntityProperties entity, string propertyName)
        {
            return GetProperties(entity).FirstOrDefault(x => x.PropertyName == propertyName);
        }

        /// <summary>
        /// Gets the property valueas stored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this IHasEntityProperties entity, string propertyName)
        {
            var entityProperty = GetProperty(entity, propertyName);
            return entityProperty?.Value;
        }

        public static void LoadProperties<T>(this List<IHasEntityProperties> entities, params string[] propertyNames) where T : FoundationEntity
        {
            var typeName = typeof(T).Name;
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            var propertyNamesAsList = propertyNames.ToList();
            var entityIds = entities.Select(x => x.Id).ToList();
            var entityProperties = entityPropertyService.Get(x =>
                    x.EntityName == typeName && propertyNamesAsList.Contains(x.PropertyName) &&
                    entityIds.Contains(x.EntityId))
                .ToList();

            foreach (var entity in entities)
            {
                entity.EntityProperties = entityProperties.Where(x => x.EntityId == entity.Id).ToList();
            }

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

            var entityProperty = GetProperty(entity, propertyName);
            if (entityProperty == null)
                return defaultValue;

            return JsonConvert.DeserializeAnonymousType(entityProperty.Value, defaultValue);
        }

        public static void SetPropertyValue(this IHasEntityProperties entity, string propertyName, object value)
        {
            //does this property exist?
            var property = GetProperty(entity, propertyName) ?? new EntityProperty()
            {
                EntityId = entity.Id,
                EntityName = entity.GetType().Name,
                PropertyName = propertyName
            };


            property.Value = JsonConvert.SerializeObject(value);
            var entityPropertyService = DependencyResolver.Resolve<IEntityPropertyService>();
            if (property.Id == 0)
            {
                entityPropertyService.Insert(property);
                entity.EntityProperties.Add(property);
            }
            else
                entityPropertyService.Update(property);
        }
    }
}