using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Infrastructure.Mvc.ModelFactories
{
    public interface IModelMapper
    {
        T Map<T>(object entity);

        T Map<T>(object entity, T existingEntity, params string[] excludeProperties);
    }
}