using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Helpers;
using EvenCart.Core.Services;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Search;

namespace EvenCart.Services.Products
{
    public class ProductService : FoundationEntityService<Product>, IProductService
    {
        private readonly IEventPublisherService _eventPublisherService;
        private readonly ISearchQueryParserService _searchQueryParserService;
        public ProductService(IEventPublisherService eventPublisherService, ISearchQueryParserService searchQueryParserService)
        {
            _eventPublisherService = eventPublisherService;
            _searchQueryParserService = searchQueryParserService;
        }

        public IList<Product> GetProducts(out int totalResults, out decimal availableFromPrice,
            out decimal availableToPrice, out Dictionary<int, string> availableManufacturers,
            out Dictionary<int, string> availableVendors, out Dictionary<string, List<string>> availableFilters,
            string searchText = null, string filterExpression = null, bool? published = true, IList<int> manufacturerIds = null,
            IList<int> vendorIds = null, IList<int> categoryIds = null, decimal? fromPrice = null,
            decimal? toPrice = null, Expression<Func<Product, object>> orderByExpression = null,
            SortOrder sortOrder = SortOrder.Descending, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            //search text
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.Name.Contains(searchText));
            }
            //published or not?
            if (published.HasValue)
            {
                query = query.Where(x => x.Published == published.Value);
            }
            //join manufacturers
            query = query.Join<Manufacturer>("ManufacturerId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Product, Manufacturer>());

            //and vendors
            query = query.Join<ProductVendor>("Id", "ProductId", joinType: JoinType.LeftOuter,
                    sourceColumnType: SourceColumn.Parent)
                .Join<Vendor>("VendorId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Product, Vendor>());

            //product specs
            query = query.Join<ProductSpecification>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductSpecificationValue>("Id", "ProductSpecificationId", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter);
            //only filterable attributes
            Expression<Func<ProductSpecification, bool>> filterWhere =
                (specification) => specification.IsFilterable || specification.AvailableAttributeId == null;
            query = query.Where(filterWhere);
            //categories?
            if (categoryIds != null && categoryIds.Any())
            {
                var asList = categoryIds.ToList();
                Expression<Func<ProductCategory, bool>> categoryWhere =
                    category => asList.Contains(category.CategoryId);
                query = query.Join<ProductCategory>("Id", "ProductId", joinType: JoinType.LeftOuter,
                        sourceColumnType: SourceColumn.Parent)
                    .Where(categoryWhere);
            }

            //now before apply where conditions for manufacturer and vendors and other product attributes, we need to 
            //find out the available filters as well.
            const string Sep = "%";
            var filterValues = query.CustomSelectNested(
                $"DISTINCT(COALESCE(ProductSpecification_Label,'') + '{Sep}' + AvailableAttribute_Name + '{Sep}' + COALESCE(ProductSpecificationValue_Label,'') + '{Sep}' + AvailableAttributeValue_Value)");
            availableFilters = new Dictionary<string, List<string>>();
            foreach (var filterValue in filterValues)
            {
                //sample row -> Shirt Size%Size%%L
                var filterRow = filterValue[0].ToString();
                if (filterRow.IsNullEmptyOrWhiteSpace())
                    continue;
                var splited = filterRow.Split(Sep);
                var filterName = string.IsNullOrEmpty(splited[0]) ? splited[1] : splited[0];
                if (!availableFilters.ContainsKey(filterName))
                    availableFilters.Add(filterName, new List<string>());
                if (!splited[2].IsNullEmptyOrWhiteSpace())
                    availableFilters[filterName].Add(splited[2]);
                availableFilters[filterName].Add(splited[3]);
            }
            //and available manufacturers
            availableManufacturers = new Dictionary<int, string>();
            var manufacturerValues = query.CustomSelectNested($"DISTINCT(Manufacturer_Name + '{Sep}' + CAST(Manufacturer_Id AS CHAR(200)))");
            foreach (var manufacturerValue in manufacturerValues)
            {
                var manufacturerRow = manufacturerValue[0].ToString();
                if (manufacturerRow.IsNullEmptyOrWhiteSpace())
                    continue;
                var splited = manufacturerRow.Split(Sep);
                var name = splited[0];
                int.TryParse(splited[1], out int manufacturerId);
                availableManufacturers.Add(manufacturerId, name);
            }

            //and available vendors
            availableVendors = new Dictionary<int, string>();
            var vendorValues = query.CustomSelectNested($"DISTINCT(Vendor_Name + '{Sep}' + CAST(Vendor_Id AS CHAR(200)))");
            foreach (var vendorValue in vendorValues)
            {
                var vendorRow = vendorValue[0].ToString();
                if (vendorRow.IsNullEmptyOrWhiteSpace())
                    continue;
                var splited = vendorRow.Split(Sep);
                var name = splited[0];
                int.TryParse(splited[1], out int vendorId);
                availableVendors.Add(vendorId, name);
            }

            //fetch the price ranges
            availableFromPrice = 0;
            availableToPrice = 0;
            var prices = query.CustomSelectNested("MIN(Product_Price), Max(Product_Price)");
            if (prices.Count > 0)
            {
                decimal.TryParse(prices[0][0].ToString(), out availableFromPrice); //start price
                decimal.TryParse(prices[0][1].ToString(), out availableToPrice); //end price
            }

            //now add the remaining conditions
            //pricing range
            if (fromPrice.HasValue)
            {
                query = query.Where(x => x.Price >= fromPrice);
            }
            if (toPrice.HasValue)
            {
                query = query.Where(x => x.Price <= toPrice);
            }

            if (filterExpression != null)
            {
                var filters = _searchQueryParserService.ParseToDictionary(filterExpression);
                var conditions = new List<LambdaExpression>();
                foreach (var filter in filters)
                {
                    var key = filter.Key;
                    var values = filter.Value;
                    Expression<Func<AvailableAttribute, ProductSpecification, AvailableAttributeValue, ProductSpecificationValue, bool>> where =
                        (aa, pa, aav, pav) => (aa.Name == key || pa.Label == key) && (values.Contains(aav.Value) || values.Contains(pav.Label));
                    conditions.Add(where);
                }
                if (conditions.Any())
                {
                    query = query.Where(conditions.CombineOr());
                }
            }
            //specific manufacturer?
            if (manufacturerIds != null && manufacturerIds.Any())
            {
                var nullableManufacturerIds = manufacturerIds.Select(x => (int?)x).ToList();
                query = query.Where(x => x.ManufacturerId != null &&
                                         nullableManufacturerIds.Contains(x.ManufacturerId));
            }
            //specific vendors
            if (vendorIds != null && vendorIds.Any())
            {
                Expression<Func<ProductVendor, bool>> vendorWhere = vendor => vendorIds.Contains(vendor.VendorId);
                query = query.Where(vendorWhere);
            }

            //and some related data as well
            Expression<Func<SeoMeta, bool>> seoMetaWhere = meta => meta.EntityName == "Product";
            query = query.Join<ProductMedia>("Id", "ProductId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", SourceColumn.Parent, JoinType.LeftOuter)
                .Where(seoMetaWhere)
                .Relate(RelationTypes.OneToMany<Product, Media>())
                .Relate(RelationTypes.OneToOne<Product, SeoMeta>());

            if (orderByExpression == null)
            {
                orderByExpression = product => product.Id;
            }
            query = query.OrderBy(orderByExpression,
                sortOrder == SortOrder.Ascending ? RowOrder.Ascending : RowOrder.Descending);
            query = query.OrderBy(x => x.DisplayOrder);
            //filter to include anything else in query
            query = _eventPublisherService.Filter(query);

            var products = query.SelectNestedWithTotalMatches(out totalResults, page, count).ToList();
            PopulateReviewSummary(products);
            return products;
        }

        public IList<Product> GetProductsByVendorIds(IList<int> vendorIds)
        {
            Expression<Func<Vendor, bool>> where = vendor => vendorIds.Contains(vendor.Id);

            return Repository
                .Join<ProductCategory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Category>("CategoryId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Join<ProductAttributeValue>("Id", "AvailableAttributeValueId", joinType: JoinType.LeftOuter)
                .Join<Manufacturer>("ManufacturerId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductVendor>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Vendor>("VendorId", "Id", SourceColumn.Chained, JoinType.LeftOuter)
                .Where(where)
                .Relate(RelationTypes.OneToMany<Product, Category>())
                .Relate(RelationTypes.OneToOne<Product, Manufacturer>())
                .Relate(RelationTypes.OneToMany<Product, Vendor>())
                .Relate(RelationTypes.OneToMany<Product, ProductAttribute>())
                .Relate<ProductAttributeValue>((product1, value) =>
                {
                    var pa = product1.ProductAttributes.First(x => x.Id == value.ProductAttributeId);
                    pa.ProductAttributeValues = pa.ProductAttributeValues ?? new List<ProductAttributeValue>();
                    if (!pa.ProductAttributeValues.Contains(value))
                        pa.ProductAttributeValues.Add(value);
                })
                .Relate<AvailableAttribute>((product1, attribute) =>
                {
                    var pa = product1.ProductAttributes.First(x => x.AvailableAttributeId == attribute.Id);
                    pa.AvailableAttribute = attribute;
                    pa.AvailableAttribute.AvailableAttributeValues = new List<AvailableAttributeValue>();
                })
                .Relate<AvailableAttributeValue>((product1, attributeValue) =>
                {
                    var pa = product1.ProductAttributes.First(x => x.AvailableAttributeId == attributeValue.AvailableAttributeId);
                    if (!pa.AvailableAttribute.AvailableAttributeValues.Contains(attributeValue))
                        pa.AvailableAttribute.AvailableAttributeValues.Add(attributeValue);
                })
                .SelectNested()
                .ToList();
        }

        public IList<int> GetProductIdsByVendorIds(IList<int> vendorIds)
        {
            return RepositoryExplorer<ProductVendor>().Where(x => vendorIds.Contains(x.VendorId))
                .Select()
                .Select(x => x.ProductId)
                .ToArray();
        }

        public IList<int> GetProductIdsByCategoryIds(IList<int> categoryIds)
        {
            return RepositoryExplorer<ProductCategory>().Where(x => categoryIds.Contains(x.CategoryId))
                .Select()
                .Select(x => x.ProductId)
                .ToArray();
        }

        public void LinkCategoryWithProduct(int categoryId, int productId, int displayOrder)
        {
            //check if already linked?
            var pc = RepositoryExplorer<ProductCategory>()
                .Where(x => x.CategoryId == categoryId && x.ProductId == productId)
                .SelectSingle();
            if (pc != null)
            {
                if (pc.DisplayOrder == displayOrder)
                    return;
                pc.DisplayOrder = displayOrder;
                EntitySet<ProductCategory>.Update(pc);
                return;
            }
            EntitySet<ProductCategory>.Insert(
                new ProductCategory() { ProductId = productId, CategoryId = categoryId, DisplayOrder = displayOrder });
        }

        public void RemoveProductCategory(int productCategoryId)
        {
            EntitySet<ProductCategory>.Delete(x => x.Id == productCategoryId);
        }

        public void RemoveProductCategories(int productId, IList<int> categoryIds)
        {
            var categoryIdsAsList = categoryIds.ToList();
            EntitySet<ProductCategory>.Delete(x => x.ProductId == productId && categoryIdsAsList.Contains(x.CategoryId));
        }

        public void LinkMediaWithProduct(int mediaId, int productId)
        {
            var productMediaCount = EntitySet<ProductMedia>.Where(x => x.MediaId == mediaId && x.ProductId == productId).Count();
            if (productMediaCount != 0)
                return;
            EntitySet<ProductMedia>.Insert(new ProductMedia() { ProductId = productId, MediaId = mediaId });
        }

        public IList<Product> GetProducts(IList<int> ids, bool withReviews = false)
        {
            Expression<Func<SeoMeta, bool>> seoMetaWhere = meta => meta.EntityName == "Product";
            var products = Repository.Where(x => ids.Contains(x.Id))
                .Join<WarehouseInventory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductMedia>("Id", "ProductId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", SourceColumn.Parent, JoinType.LeftOuter)
                .Where(seoMetaWhere)
                .Relate(RelationTypes.OneToMany<Product, Media>())
                .Relate(RelationTypes.OneToOne<Product, SeoMeta>())
                .Relate(RelationTypes.OneToMany<Product, WarehouseInventory>())
                .SelectNested()
                .ToList();
            if (withReviews)
                PopulateReviewSummary(products);
            return products;
        }

        public override Product Get(int id)
        {
            Expression<Func<SeoMeta, bool>> seoMetaWhere = meta => meta.EntityName == "Product";

            var product = Repository.Where(x => x.Id == id)
                .Join<WarehouseInventory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductCategory>("Id", "ProductId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Category>("CategoryId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductMedia>("Id", "ProductId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductAttributeValue>("Id", "ProductAttributeId", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductSpecification>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductSpecificationValue>("Id", "ProductSpecificationId", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductSpecificationGroup>("ProductSpecificationGroupId", "Id", typeof(ProductSpecification), joinType: JoinType.LeftOuter)
                .Join<Manufacturer>("ManufacturerId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductVendor>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Vendor>("VendorId", "Id", SourceColumn.Chained, JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", SourceColumn.Parent, JoinType.LeftOuter)
                .Where(seoMetaWhere)
                .Relate(RelationTypes.OneToOne<Product, SeoMeta>())
                .Relate<ProductCategory>((product1, category) =>
                {
                    //we don't have categories as yet, so we'll have to preserve display order
                    product1.Tag = product1.Tag ?? new Dictionary<int, int>();
                    var dictionary = (Dictionary<int, int>)product1.Tag;
                    if (dictionary.ContainsKey(category.CategoryId))
                        return;
                    dictionary.Add(category.CategoryId, category.DisplayOrder);
                })
                .Relate(RelationTypes.OneToMany<Product, Category>((product1, category) =>
                {
                    //we need to est display order of category appropriately from product1 object's tag property
                    //see the relation ProductCategory below
                    if (product1.Tag == null)
                        return;
                    var dictionary = (Dictionary<int, int>)product1.Tag;
                    if (dictionary.ContainsKey(category.Id))
                        category.DisplayOrder = dictionary[category.Id];
                }))
                .Relate(RelationTypes.OneToMany<Product, Media>())
                .Relate(RelationTypes.OneToMany<Product, ProductAttribute>())
                .Relate(RelationTypes.OneToMany<Product, ProductSpecification>())
                .Relate<ProductAttributeValue>((product1, value) =>
                {
                    var pa = product1.ProductAttributes.FirstOrDefault(x => x.Id == value.ProductAttributeId);
                    if (pa == null)
                        return;
                    pa.ProductAttributeValues = pa.ProductAttributeValues ?? new List<ProductAttributeValue>();
                    if (!pa.ProductAttributeValues.Contains(value))
                        pa.ProductAttributeValues.Add(value);
                })
                .Relate<ProductSpecificationValue>((product1, value) =>
                {
                    var pa = product1.ProductSpecifications.FirstOrDefault(x => x.Id == value.ProductSpecificationId);
                    if (pa == null)
                        return;
                    pa.ProductSpecificationValues = pa.ProductSpecificationValues ?? new List<ProductSpecificationValue>();
                    if (!pa.ProductSpecificationValues.Contains(value))
                        pa.ProductSpecificationValues.Add(value);
                })
                .Relate<ProductSpecificationGroup>((product1, group) =>
                {
                    product1.ProductSpecifications.FirstOrDefault(x => x.ProductSpecificationGroupId == group.Id)
                        .ProductSpecificationGroup = group;
                })
                .Relate<AvailableAttribute>((product1, attribute) =>
                {
                    var pa = product1.ProductAttributes.FirstOrDefault(x => x.AvailableAttributeId == attribute.Id);
                    if (pa != null)
                    {
                        pa.AvailableAttribute = attribute;
                        if (pa.Label.IsNullEmptyOrWhiteSpace())
                        {
                            pa.Label = attribute.Name;
                        }
                        pa.AvailableAttribute.AvailableAttributeValues = pa.AvailableAttribute.AvailableAttributeValues ?? new List<AvailableAttributeValue>();
                    }


                    var ps = product1.ProductSpecifications?.FirstOrDefault(x => x.AvailableAttributeId == attribute.Id);
                    if (ps == null)
                        return;
                    ps.AvailableAttribute = attribute;
                    if (ps.Label.IsNullEmptyOrWhiteSpace())
                    {
                        ps.Label = attribute.Name;
                    }
                    ps.AvailableAttribute.AvailableAttributeValues = ps.AvailableAttribute.AvailableAttributeValues ?? new List<AvailableAttributeValue>();
                })
                .Relate<AvailableAttributeValue>((product1, attributeValue) =>
                {
                    var pa =
                        product1.ProductAttributes.FirstOrDefault(
                            x => x.AvailableAttributeId == attributeValue.AvailableAttributeId);
                    if (pa != null && pa.AvailableAttribute != null)
                    {
                        if (!pa.AvailableAttribute.AvailableAttributeValues.Contains(attributeValue))
                            pa.AvailableAttribute.AvailableAttributeValues.Add(attributeValue);
                    }

                    var ps =
                        product1.ProductSpecifications?.FirstOrDefault(
                            x => x.AvailableAttributeId == attributeValue.AvailableAttributeId);
                    if (ps?.AvailableAttribute != null)
                    {
                        if (!ps.AvailableAttribute.AvailableAttributeValues.Contains(attributeValue))
                        {
                            ps.AvailableAttribute.AvailableAttributeValues.Add(attributeValue);
                        }

                        var psValue = ps.ProductSpecificationValues.FirstOrDefault(
                            x => x.AvailableAttributeValueId == attributeValue.Id);
                        if (psValue != null)
                        {
                            psValue.AvailableAttributeValue = attributeValue;
                            psValue.Label = psValue.Label ?? psValue.AvailableAttributeValue.Value;
                        }
                    }
                })
                .Relate(RelationTypes.OneToOne<Product, Manufacturer>())
                .Relate(RelationTypes.OneToMany<Product, Vendor>())
                .SelectNested()
                .FirstOrDefault();
            PopulateReviewSummary(new List<Product>() { product });
            return product;
        }

        public void PopulateReviewSummary(IList<Product> products)
        {
            if (!products.Any())
                return;
            var productIdStr = string.Join(",", products.Select(x => x.Id));
            //find the average rating
            using (var multiresult = EntitySet.Query(
                $"SELECT ProductId, AVG(CAST(Rating AS DECIMAL(10,2))) AS AverageRating, COUNT(Rating) AS TotalRatings FROM {DotEntityDb.GetTableNameForType<Review>()} WHERE ProductId IN ({productIdStr}) GROUP BY ProductId;" +
                $"SELECT ProductId, COUNT(Rating) AS TotalReviews FROM {DotEntityDb.GetTableNameForType<Review>()} WHERE ProductId IN ({productIdStr}) AND (Title IS NOT NULL OR Description IS NOT NULL)  GROUP BY ProductId;",
                null))
            {
                var reviewSummaryData = multiresult.SelectAllAs<Product.ReviewSummaryData>();
                foreach (var summaryData in reviewSummaryData)
                {
                    var product = products.FirstOrDefault(x => x.Id == summaryData.ProductId);
                    if (product != null)
                        product.ReviewSummary = summaryData;
                }
                reviewSummaryData = multiresult.SelectAllAs<Product.ReviewSummaryData>();
                foreach (var summaryData in reviewSummaryData)
                {
                    var productSummaryData = products.FirstOrDefault(x => x.Id == summaryData.ProductId)?.ReviewSummary;
                    if (productSummaryData != null)
                        productSummaryData.TotalReviews = summaryData.TotalReviews;
                }

            }
        }

        public void UpdatePopularityIndex(bool increment = true, params int[] productIds)
        {
            if (!productIds.Any())
                return;
            var productIdStr = string.Join(",", productIds);
            var piColumn = nameof(Product.PopularityIndex);
            var direction = increment ? "+" : "-";
            using (EntitySet.Query($"UPDATE {DotEntityDb.GetTableNameForType<Product>()} SET {piColumn} = {piColumn} {direction} 1 WHERE Id IN ({productIdStr})", null))
            {

            }
        }

        public IList<Product> GetProductsWithVariants(out int totalResults, string searchText = null, bool? published = null, bool? trackInventory = null, Expression<Func<Product, object>> orderByExpression = null,
            SortOrder sortOrder = SortOrder.Descending, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
            {
                query = query.Where(x => x.Name.Contains(searchText));
            }
            if (published.HasValue)
            {
                query = published.Value ? query.Where(x => x.Published) : query.Where(x => !x.Published);
            }
            if (trackInventory.HasValue)
            {
                query = trackInventory.Value ? query.Where(x => x.TrackInventory) : query.Where(x => !x.TrackInventory);
            }
         
            return query
                .Join<WarehouseInventory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariant>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", typeof(ProductVariantAttribute), JoinType.LeftOuter)
                .Join<ProductAttributeValue>("ProductAttributeValueId", "Id", typeof(ProductVariantAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", typeof(ProductAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", typeof(ProductAttributeValue),
                    joinType: JoinType.LeftOuter)
                .Join<WarehouseInventory>("Id", "ProductVariantId", typeof(ProductVariant), joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Product, ProductVariant>())
                .Relate<ProductVariantAttribute>((product, attribute) =>
                {
                    var variant = product.ProductVariants.FirstOrDefault(x => x.Id == attribute.ProductVariantId);
                    if (variant != null)
                    {
                        attribute.ProductVariant = variant;
                        variant.ProductVariantAttributes =
                            variant.ProductVariantAttributes ?? new List<ProductVariantAttribute>();
                        if (!variant.ProductVariantAttributes.Contains(attribute))
                            variant.ProductVariantAttributes.Add(attribute);
                    }
                })
                .Relate<ProductAttribute>((product, attribute) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes))
                    {
                        if (variantAttribute.ProductAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute = attribute;
                        }
                    }
                })
                .Relate<ProductAttributeValue>((product, attributeValue) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue = attributeValue;
                        }
                    }
                })
                .Relate<AvailableAttribute>((product, attribute) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttribute.AvailableAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute.AvailableAttribute = attribute;
                            if (variantAttribute.ProductAttribute.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttribute.Label = attribute.Name;
                        }
                    }
                })
                .Relate<AvailableAttributeValue>((product, attributeValue) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttributeValue.AvailableAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue.AvailableAttributeValue = attributeValue;
                            if (variantAttribute.ProductAttributeValue.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttributeValue.Label = attributeValue.Value;
                        }
                    }
                })
                .OrderBy(orderByExpression, sortOrder == SortOrder.Ascending ? RowOrder.Ascending : RowOrder.Descending)
                .SelectNestedWithTotalMatches(out totalResults, page, count)
                .ToList();
        }

        public IList<Product> GetProductsWithVariants(IList<int> ids)
        {
            return Repository
                .Where(x => ids.Contains(x.Id))
                .Join<WarehouseInventory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariant>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", typeof(ProductVariantAttribute), JoinType.LeftOuter)
                .Join<ProductAttributeValue>("ProductAttributeValueId", "Id", typeof(ProductVariantAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", typeof(ProductAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", typeof(ProductAttributeValue),
                    joinType: JoinType.LeftOuter)
                .Join<WarehouseInventory>("Id", "ProductVariantId", typeof(ProductVariant), joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Product, ProductVariant>())
                .Relate<ProductVariantAttribute>((product, attribute) =>
                {
                    var variant = product.ProductVariants.FirstOrDefault(x => x.Id == attribute.ProductVariantId);
                    if (variant != null)
                    {
                        attribute.ProductVariant = variant;
                        variant.ProductVariantAttributes =
                            variant.ProductVariantAttributes ?? new List<ProductVariantAttribute>();
                        if (!variant.ProductVariantAttributes.Contains(attribute))
                            variant.ProductVariantAttributes.Add(attribute);
                    }
                })
                .Relate<ProductAttribute>((product, attribute) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes))
                    {
                        if (variantAttribute.ProductAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute = attribute;
                        }
                    }
                })
                .Relate<ProductAttributeValue>((product, attributeValue) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue = attributeValue;
                        }
                    }
                })
                .Relate<AvailableAttribute>((product, attribute) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttribute.AvailableAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute.AvailableAttribute = attribute;
                            if (variantAttribute.ProductAttribute.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttribute.Label = attribute.Name;
                        }
                    }
                })
                .Relate<AvailableAttributeValue>((product, attributeValue) =>
                {
                    foreach (var variantAttribute in product.ProductVariants.SelectMany(x => x.ProductVariantAttributes)
                    )
                    {
                        if (variantAttribute.ProductAttributeValue.AvailableAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue.AvailableAttributeValue = attributeValue;
                            if (variantAttribute.ProductAttributeValue.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttributeValue.Label = attributeValue.Value;
                        }
                    }
                })
                .SelectNested()
                .ToList();
        }
    }
}