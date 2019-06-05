using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IProductAccountant
    {
        StockStatus GetStockStatus(Product product, IList<int> productAttributeValueIds, out ProductVariant variant);

        void UpdateStockStatusByOrder(Order order);
    }
}