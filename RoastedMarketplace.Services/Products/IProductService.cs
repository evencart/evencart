using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductService : IFoundationEntityService<Product>
    {
        IEnumerable<Product> GetProducts(string searchText = null, bool onlyPublished = true, int[] manufacturerIds = null, int[] vendorIds = null, int[] categoryids = null, int page = 1, int count = int.MaxValue);

        IList<Product> GetProductsByVendorIds(int[] vendorIds);

        int[] GetProductIdsByVendorIds(int[] vendorIds);

        int[] GetProductIdsByCategoryIds(int[] categoryIds);

        void LinkCategoryWithProduct(int categoryId, int productId);

        void RemoveProductCategory(int productCategoryId);
    }
}