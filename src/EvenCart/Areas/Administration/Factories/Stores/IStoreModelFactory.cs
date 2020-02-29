using EvenCart.Areas.Administration.Models.Store;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Stores
{
    public interface IStoreModelFactory : IModelFactory<Store, StoreModel>
    {
        
    }
}