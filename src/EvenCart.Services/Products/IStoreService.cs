using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IStoreService : IFoundationEntityService<Store>
    {
        Store GetByDomain(string domain);

        Store CloneStore(Store store, string newStoreName, string domain);
    }
}