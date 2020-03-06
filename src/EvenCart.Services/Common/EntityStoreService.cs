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