using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Services.Common
{
    public interface IEntityStoreService : IFoundationEntityService<EntityStore>
    {
        void SetEntityStores<T>(int id, IList<int> storeIds) where T : FoundationEntity; 
    }
}