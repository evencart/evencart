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

using System;
using System.Collections.Concurrent;
using System.Linq;

namespace EvenCart.Infrastructure.Mvc.ModelFactories
{
    public class ModelMapper : IModelMapper
    {
        private readonly ConcurrentDictionary<TypePair, Func<object, object, object>> _mappingCache = new ConcurrentDictionary<TypePair, Func<object, object, object>>();
        
        public T Map<T>(object entity)
        {
            return MapImpl<T>(entity, default(T));
        }

        public T Map<T>(object entity, T existingEntity, params string[] excludeProperties)
        {
            if (existingEntity == null)
                throw new ArgumentNullException(nameof(existingEntity));
            return MapImpl<T>(entity, existingEntity, excludeProperties);
        }

        public object MapType(Type targetType, object entity)
        {
            return MapImplType(targetType, entity, null);
        }

        public object MapType(Type targetType, object entity, object existingEntity, params string[] excludeProperties)
        {
            return MapImplType(targetType, entity, existingEntity, excludeProperties);
        }

        private T MapImpl<T>(object entity, T existingEntity, params string[] excludeProperties)
        {
            return (T) MapImplType(typeof(T), entity, existingEntity, excludeProperties);
        }

        private object MapImplType(Type targetType, object entity, object existingEntity, params string[] excludeProperties)
        {
            if (entity == null)
                return null;
            var typeOfObject = entity.GetType();
            var typePair = TypePair.Create(entity.GetType(), targetType);
            if (!_mappingCache.TryGetValue(typePair, out Func<object, object, object> action))
            {
                var typeOfT = targetType;
                action = (givenObject, existingTObject) =>
                {
                    //get properties of T which have get 
                    var entityProperties = typeOfObject.GetProperties();
                    var modelProperties = typeOfT.GetProperties();

                    //create model
                    var model = existingTObject != null ? existingTObject : Activator.CreateInstance(targetType);
                    foreach (var modelProperty in modelProperties)
                    {
                        if (!modelProperty.CanWrite)
                            continue;
                        //are there any matching properties
                        var ep = entityProperties.FirstOrDefault(x => x.Name == modelProperty.Name && x.PropertyType == modelProperty.PropertyType);
                        if (ep == null || excludeProperties.Contains(ep.Name))
                            continue;
                        //get instance value for the property

                        var epValue = ep.GetValue(givenObject);
                        modelProperty.SetValue(model, epValue);
                    }
                    return model;
                };
                _mappingCache.TryAdd(typePair, action);
            }
            return action(entity, existingEntity);
        }
    }
}