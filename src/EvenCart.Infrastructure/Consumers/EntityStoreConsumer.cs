using EvenCart.Core.Data;
using EvenCart.Core.Services.Events;
using EvenCart.Services.Common;

namespace EvenCart.Infrastructure.Consumers
{
    public class EntityStoreConsumer<T> : IFoundationEntityInserted<T>, IFoundationEntityUpdated<T>, IFoundationEntityDeleted<T> where T : FoundationEntity
    {
        private readonly IEntityStoreService _entityStoreService;
        public EntityStoreConsumer(IEntityStoreService entityStoreService)
        {
            _entityStoreService = entityStoreService;
        }

        public void OnInserted(T entity)
        {
            var entityAsStoreEntity = entity as IStoreEntity;
            if (entityAsStoreEntity == null)
                return;
            _entityStoreService.SetEntityStores<T>(entity.Id, entityAsStoreEntity.StoreIds);
        }

        public void OnUpdated(T entity)
        {
            OnInserted(entity);
        }

        public void OnDeleted(T entity)
        {
            var typeName = typeof(T).Name;
            _entityStoreService.Delete(x => x.EntityId == entity.Id && x.EntityName == typeName);
        }
    }
}