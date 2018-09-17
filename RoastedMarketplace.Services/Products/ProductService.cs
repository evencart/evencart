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
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class ProductService : FoundationEntityService<Product>, IProductService
    {
        public IEnumerable<Product> GetProducts(string searchText = null, bool onlyPublished = true, int[] manufacturerIds = null, int[] vendorIds = null, int[] categoryids = null, int page = 1, int count = int.MaxValue)
        {
            var query = Repository;
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }
            if (onlyPublished)
            {
                query = query.Where(x => x.Published);
            }
            if (manufacturerIds != null && manufacturerIds.Any())
            {
                query = query.Where(x => x.ManufacturerId != null && manufacturerIds.Contains(x.ManufacturerId.Value));
            }
            return query.Select(page, count);
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

        public void LinkCategoryWithProduct(int categoryId, int productId)
        {
            //check if already linked?
            var pc = RepositoryExplorer<ProductCategory>()
                .Where(x => x.CategoryId == categoryId && x.ProductId == productId)
                .SelectSingle();
            if (pc != null)
                return;
            EntitySet<ProductCategory>.Insert(new ProductCategory() {ProductId = productId, CategoryId = categoryId});
        }

        public void RemoveProductCategory(int productCategoryId)
        {
            EntitySet<ProductCategory>.Delete(x => x.Id == productCategoryId);
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
                .Relate(RelationTypes.OneToMany<Product, Category>())
                .Relate(RelationTypes.OneToMany<Product, Media>())
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
                .FirstOrDefault();

            return product;
        }


    }
}