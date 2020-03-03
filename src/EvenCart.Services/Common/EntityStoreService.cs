using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Services.Common
{
    public class EntityStoreService : FoundationEntityService<EntityStore>, IEntityStoreService
    {
        public void SetEntityStores<T>(int id, IList<int> storeIds) where T : FoundationEntity
        {
            storeIds = storeIds ?? new List<int>();
            //get existing roles
            var entityName = typeof(T).Name;
            var savedEntityStores = Get(x => x.EntityName == entityName && x.EntityId == id).ToList();
            var toDelete = savedEntityStores.Where(x => !storeIds.Contains(x.StoreId));
            var toInsert = storeIds.Where(x => savedEntityStores.All(y => y.StoreId != x)).Select(x => new EntityStore()
            {
                EntityId = id,
                EntityName = entityName,
                StoreId = x
            });
            Transaction.Initiate(transaction =>
            {
                foreach (var e in toDelete)
                    Delete(e, transaction);
                foreach (var e in toInsert)
                    Insert(e, transaction);
            });
        }
    }
}