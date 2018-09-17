using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace RoastedMarketplace.Infrastructure.Mvc.ModelFactories
{
    public class ModelMapper : IModelMapper
    {
        private readonly ConcurrentDictionary<TypePair, Func<object>> _mappingCache = new ConcurrentDictionary<TypePair, Func<object>>();

        public T Map<T>(object entity)
        {
            if (entity == null)
                return default(T);
            var typeOfObject = entity.GetType();
            var typePair = TypePair.Create(entity.GetType(), typeof(T));
            if (!_mappingCache.TryGetValue(typePair, out Func<object> action))
            {
                var typeOfT = typeof(T);
                action = () =>
                {
                    //get properties of T which have get 
                    var entityProperties = typeOfObject.GetProperties();
                    var modelProperties = typeOfT.GetProperties();

                    //create model
                    var model = Activator.CreateInstance<T>();
                    foreach (var modelProperty in modelProperties)
                    {
                        //are there any matching properties
                        var ep = entityProperties.FirstOrDefault(x => x.Name == modelProperty.Name);
                        if (ep == null)
                            continue;
                        //get instance value for the property

                        var epValue = ep.GetValue(entity);
                        modelProperty.SetValue(model, epValue);
                    }
                    return model;
                };
                _mappingCache.TryAdd(typePair, action);
            }
            return (T) action();
        }
    }
}