using System.Collections.Generic;

namespace EvenCart.Core.Data
{
    public interface IStoreEntity
    {
        IList<int> StoreIds { get; set; }
    }
}