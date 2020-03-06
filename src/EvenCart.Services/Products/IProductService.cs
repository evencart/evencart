#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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
            bool? published = true, int? storeId = null, IList<string> tags = null, IList<int> manufacturerIds = null, IList<int> vendorIds = null,
            IList<int> catalogIds = null,
            IList<int> categoryids = null, IList<int> roleIds = null, bool ignoreRoles = false, decimal? fromPrice = null, decimal? toPrice = null,
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

        void SetProductCatalogs(int productId, IList<int> catalogIds);
    }
}