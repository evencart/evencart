using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductService : IFoundationEntityService<Product>
    {
        IEnumerable<Product> GetProducts(out int totalResults, string searchText = null, bool? published = true, int[] manufacturerIds = null, int[] vendorIds = null, int[] categoryids = null, Expression<Func<Product, object>> orderByExpression = null, SortOrder sortOrder = SortOrder.Descending, int page = 1, int count = int.MaxValue);

        IList<Product> GetProductsByVendorIds(int[] vendorIds);

        int[] GetProductIdsByVendorIds(int[] vendorIds);

        int[] GetProductIdsByCategoryIds(int[] categoryIds);

        void LinkCategoryWithProduct(int categoryId, int productId, int displayOrder);

        void RemoveProductCategory(int productCategoryId);

        void RemoveProductCategories(int productId, int[] categoryIds);

        void LinkMediaWithProduct(int mediaId, int productId);

    }
}