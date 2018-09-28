using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class ProductService : FoundationEntityService<Product>, IProductService
    {
        public IEnumerable<Product> GetProducts(out int totalResults, string searchText = null, bool? published = true, int[] manufacturerIds = null,
            int[] vendorIds = null, int[] categoryids = null, Expression<Func<Product, object>> orderByExpression = null, SortOrder sortOrder = SortOrder.Descending, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.Name.Contains(searchText));
            }
            if (published.HasValue)
            {
                query = query.Where(x => x.Published == published.Value);
            }
            if (manufacturerIds != null && manufacturerIds.Any())
            {
                query = query.Where(x => x.ManufacturerId != null && manufacturerIds.Contains(x.ManufacturerId.Value));
            }
            if (orderByExpression == null)
            {
                orderByExpression = product => product.Id;
                query = query.OrderBy(orderByExpression,
                    sortOrder == SortOrder.Ascending ? RowOrder.Ascending : RowOrder.Descending);
            }

            return query.SelectWithTotalMatches(out totalResults, page, count);
        }

        public IList<Product> GetProductsByVendorIds(int[] vendorIds)
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

        public int[] GetProductIdsByVendorIds(int[] vendorIds)
        {
            return EntitySet<ProductVendor>.Where(x => vendorIds.Contains(x.VendorId))
                .Select()
                .Select(x => x.ProductId)
                .ToArray();
        }

        public int[] GetProductIdsByCategoryIds(int[] categoryIds)
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
                new ProductCategory() {ProductId = productId, CategoryId = categoryId, DisplayOrder = displayOrder});
        }

        public void RemoveProductCategory(int productCategoryId)
        {
            EntitySet<ProductCategory>.Delete(x => x.Id == productCategoryId);
        }

        public void RemoveProductCategories(int productId, int[] categoryIds)
        {
            var categoryIdsAsList = categoryIds.ToList();
            EntitySet<ProductCategory>.Delete(x => x.ProductId == productId && categoryIdsAsList.Contains(x.CategoryId));
        }

        public void LinkMediaWithProduct(int mediaId, int productId)
        {
            var productMediaCount = EntitySet<ProductMedia>.Where(x => x.MediaId == mediaId && x.ProductId == productId).Count();
            if (productMediaCount != 0)
                return;
            EntitySet<ProductMedia>.Insert(new ProductMedia() {ProductId = productId, MediaId = mediaId});
        }

        public override Product Get(int id)
        {
            var product = Repository.Where(x => x.Id == id)
                .Join<ProductCategory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Category>("CategoryId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductMedia>("Id", "ProductId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Join<ProductAttributeValue>("Id", "AvailableAttributeValueId", joinType: JoinType.LeftOuter)
                .Join<Manufacturer>("ManufacturerId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<ProductVendor>("Id", "ProductId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Vendor>("VendorId", "Id", SourceColumn.Chained, JoinType.LeftOuter)
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
                .Relate(RelationTypes.OneToOne<Product, Manufacturer>())
                .Relate(RelationTypes.OneToMany<Product, Vendor>())
                .Relate(RelationTypes.OneToMany<Product, ProductAttribute>())
                .Relate<ProductCategory>((product1, category) =>
                {
                    //we don't have categories as yet, so we'll have to preserve display order
                    product1.Tag = product1.Tag ?? new Dictionary<int, int>();
                    var dictionary = (Dictionary<int, int>) product1.Tag;
                    if (dictionary.ContainsKey(category.CategoryId))
                        return;
                    dictionary.Add(category.CategoryId, category.DisplayOrder);
                })
                .Relate<ProductAttributeValue>((product1, value) =>
                {
                    var pa = product1.ProductAttributes.FirstOrDefault(x => x.Id == value.ProductAttributeId);
                    if (pa == null)
                        return;
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
                .FirstOrDefault();

            return product;
        }


    }
}