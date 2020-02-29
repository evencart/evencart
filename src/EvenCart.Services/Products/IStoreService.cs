using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IStoreService : IFoundationEntityService<Store>
    {
        Store GetByDomain(string domain);

        void CloneStore(Store store, string newStoreName, string domain);
    }
}