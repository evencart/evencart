using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Data;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.Filters
{
    public class StoreEntityFilter<T> : IFoundationFilter<IEntitySet<T>> where T : FoundationEntity, IStoreEntity
    {
        //for some entities like Catalog, we need to show them all the time no matter what, just storing them in a list for generalized access
        private IList<Type> _alwaysAllTypes = new List<Type>()
        {
            typeof(Catalog),
            typeof(Menu)
        };
        public IEntitySet<T> Filter(IEntitySet<T> entity)
        {
            var entityName = typeof(T).Name;
            Expression<Func<EntityStore, bool>> storeWhere = x => true;

            if (!_alwaysAllTypes.Contains(typeof(T)))
            {
                //restrict to current store
                var storeId = ApplicationEngine.CurrentStore?.Id ?? 0;
                var allowedStoreIds = new List<int> { storeId, 0 };
                storeWhere = store => store.StoreId == null || allowedStoreIds.Contains(store.StoreId);
            }

            entity.Join<EntityStore>("Id", "EntityId", SourceColumn.Parent, JoinType.LeftOuter, (storeEntity, store) =>
                    store.EntityName == entityName)
                .Join<Store>("StoreId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<T, Store>())
                .Where(storeWhere);
            return entity;
        }
    }
}