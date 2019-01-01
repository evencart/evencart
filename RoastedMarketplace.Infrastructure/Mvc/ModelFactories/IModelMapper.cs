using System;

namespace RoastedMarketplace.Infrastructure.Mvc.ModelFactories
{
    public interface IModelMapper
    {
        T Map<T>(object entity);

        T Map<T>(object entity, T existingEntity, params string[] excludeProperties);

        object MapType(Type targetType, object entity);

        object MapType(Type targetType, object entity, object existingEntity, params string[] excludeProperties);
    }
}