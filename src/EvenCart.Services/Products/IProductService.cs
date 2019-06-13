using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Products
{
    public interface IProductService : IFoundationEntityService<Product>
    {
        IList<Product> GetProducts(out int totalResults, out decimal availableFromPrice,
            out decimal availableToPrice, 
            out Dictionary<int, string> availableManufacturers, 
            out Dictionary<int, string> availableVendors, 
            out Dictionary<string, List<string>> availableFilters, 
            string searchText = null,
            string filterExpression = null,
            bool? published = true, IList<int> manufacturerIds = null, IList<int> vendorIds = null,
            IList<int> categoryids = null, decimal? fromPrice = null, decimal? toPrice = null,
            Expression<Func<Product, object>> orderByExpression = null, SortOrder sortOrder = SortOrder.Descending,
            int page = 1, int count = int.MaxValue);

        IList<Product> GetProductsByVendorIds(IList<int> vendorIds);

        IList<int> GetProductIdsByVendorIds(IList<int> vendorIds);

        IList<int> GetProductIdsByCategoryIds(IList<int> categoryIds);

        void LinkCategoryWithProduct(int categoryId, int productId, int displayOrder);

        void RemoveProductCategory(int productCategoryId);

        void RemoveProductCategories(int productId, IList<int> categoryIds);

        void LinkMediaWithProduct(int mediaId, int productId);

        IList<Product> GetProducts(IList<int> ids, bool withReviews = false);

        void PopulateReviewSummary(IList<Product> products);

        void UpdatePopularityIndex(bool increment = true, params int[] productIds);

        IList<Product> GetProductsWithVariants(out int totalResults, int warehouseId, string searchText = null, bool? published = null, bool? trackInventory = null, int page = 1, int count = int.MaxValue);

        IList<Product> GetProductsWithVariants(IList<int> ids);
    }
}