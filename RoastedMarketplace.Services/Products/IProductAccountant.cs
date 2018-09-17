using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductAccountant
    {
        StockStatus GetStockStatus(Product product, IList<int> productAttributeValueIds, out ProductVariant variant);
    }
}