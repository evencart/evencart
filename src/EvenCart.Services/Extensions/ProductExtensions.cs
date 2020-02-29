using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Common;

namespace EvenCart.Services.Extensions
{
    public static class ProductExtensions
    {
        /// <summary>
        /// Checks if a product is available in public store
        /// </summary>
        public static bool IsPublic(this Product product, int storeId)
        {
            var catalogIds = product?.Catalogs?.Select(x => x.Id).ToList() ?? new List<int>();
            if (!catalogIds.Any())
                return false; //no catalog means product is not available in any catalog
            var entityStoresService = DependencyResolver.Resolve<IEntityStoreService>();
            var catalogStores = entityStoresService.Get(x => x.EntityName == nameof(Catalog) && x.StoreId == storeId && catalogIds.Contains(x.EntityId));
            if (!catalogStores.Any())
                return false;
            return product != null && product.Published && !product.Deleted;
        }
    }
}