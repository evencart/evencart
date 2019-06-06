using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IWarehouseInventoryService : IFoundationEntityService<WarehouseInventory>
    {
        IEnumerable<WarehouseInventory> GetByProduct(int productId, int? warehouseId = null);

        IEnumerable<WarehouseInventory> GetByProducts(IList<int> productIds, int? warehouseId = null);
    }
}