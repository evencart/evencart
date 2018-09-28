using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RoastedMarketplace.Infrastructure.Mvc.ModelFactories
{
    public class ModelMapper : IModelMapper
    {
        private readonly ConcurrentDictionary<TypePair, Func<object, object, object>> _mappingCache = new ConcurrentDictionary<TypePair, Func<object, object, object>>();

        public T Map<T>(object entity, T existingTEntity = default(T))
        {
            if (entity == null)
                return default(T);
            var typeOfObject = entity.GetType();
            var typePair = TypePair.Create(entity.GetType(), typeof(T));
            if (!_mappingCache.TryGetValue(typePair, out Func<object, object, object> action))
            {
                var typeOfT = typeof(T);
                action = (givenObject, existingTObject) =>
                {
                    //get properties of T which have get 
                    var entityProperties = typeOfObject.GetProperties();
                    var modelProperties = typeOfT.GetProperties();

                    //create model
                    var model = existingTObject != null ? (T) existingTObject : Activator.CreateInstance<T>();
                    foreach (var modelProperty in modelProperties)
                    {
                        //are there any matching properties
                        var ep = entityProperties.FirstOrDefault(x => x.Name == modelProperty.Name && x.PropertyType == modelProperty.PropertyType);
                        if (ep == null)
                            continue;
                        //get instance value for the property

                        var epValue = ep.GetValue(givenObject);
                        modelProperty.SetValue(model, epValue);
                    }
                    return model;
                };
                _mappingCache.TryAdd(typePair, action);
            }
            return (T) action(entity, existingTEntity);
        }
    }
}