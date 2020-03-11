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