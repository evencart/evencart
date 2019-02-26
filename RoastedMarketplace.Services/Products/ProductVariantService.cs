using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class ProductVariantService : FoundationEntityService<ProductVariant>, IProductVariantService
    {
        private readonly IProductService _productService;
        public ProductVariantService(IProductService productService)
        {
            _productService = productService;
        }

        public void DeleteVariantsByProductAttributeValueId(int productAttributeValueId, Transaction transaction = null)
        {
            //first get product variant attributes
            var productVariantAttributes = EntitySet<ProductVariantAttribute>
                .Where(x => x.ProductAttributeValueId == productAttributeValueId)
                .Select();

            //get all variants ids
            var productVariantIds = productVariantAttributes.Select(x => x.ProductVariantId).Distinct().ToList();
            if (productVariantIds.Any())
                //delete attributes
                Delete(x => productVariantIds.Contains(x.Id));
        }

        public void DeleteVariantsByProductAttributeId(int productAttributeId, Transaction transaction = null)
        {
            Expression<Func<ProductAttribute, bool>> where = attribute => attribute.Id == productAttributeId;

            var productVariants = Repository.Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Where(where)
                .SelectNested()
                .ToList();

            foreach(var variant in productVariants)
                Delete(variant, transaction);
        }

        public ProductVariant AddVariant(Product product, ProductVariant productVariant, Transaction transaction = null)
        {
          
            Insert(productVariant, transaction);

            foreach (var variantAttribute in productVariant.ProductVariantAttributes)
                variantAttribute.ProductVariantId = productVariant.Id;

            EntitySet<ProductVariantAttribute>.Insert(productVariant.ProductVariantAttributes.ToArray());

            //update product
            if (!product.HasVariants)
            {
                product.HasVariants = true;
                _productService.Update(product);
            }
            return productVariant;
        }

        public ProductVariant GetByAttributeValueIds(IList<int> productAttributeValueIds)
        {
            //let's create a custom query here
            var variantIdFetcher =
                $"select top 1 productvariantid from productvariantattribute where productattributevalueid in ({string.Join(",", productAttributeValueIds)}) group by productvariantid HAVING COUNT(*) = {productAttributeValueIds.Count} order by count(*) desc";

            var sql =
                $"SELECT t2.[ProductVariantId] AS ProductVariantAttribute_ProductVariantId,t2.[ProductAttributeId] AS ProductVariantAttribute_ProductAttributeId,t2.[ProductAttributeValueId] AS ProductVariantAttribute_ProductAttributeValueId,t2.[Id] AS ProductVariantAttribute_Id,t3.[ProductId] AS ProductAttribute_ProductId,t3.[AvailableAttributeId] AS ProductAttribute_AvailableAttributeId,t3.[InputFieldType] AS ProductAttribute_InputFieldType,t3.[Label] AS ProductAttribute_Label,t3.[DisplayOrder] AS ProductAttribute_DisplayOrder,t3.[IsRequired] AS ProductAttribute_IsRequired,t3.[Id] AS ProductAttribute_Id,t4.[ProductAttributeId] AS ProductAttributeValue_ProductAttributeId,t4.[AvailableAttributeValueId] AS ProductAttributeValue_AvailableAttributeValueId,t4.[Label] AS ProductAttributeValue_Label,t4.[Id] AS ProductAttributeValue_Id,t1.[ProductId] AS ProductVariant_ProductId,t1.[Sku] AS ProductVariant_Sku,t1.[Gtin] AS ProductVariant_Gtin,t1.[Mpn] AS ProductVariant_Mpn,t1.[Price] AS ProductVariant_Price,t1.[StockQuantity] AS ProductVariant_StockQuantity,t1.[TrackInventory] AS ProductVariant_TrackInventory,t1.[CanOrderWhenOutOfStock] AS ProductVariant_CanOrderWhenOutOfStock,t1.[Id] AS ProductVariant_Id FROM [ProductVariant] t1 LEFT OUTER JOIN [ProductVariantAttribute] t2 ON t1.[Id] = t2.[ProductVariantId] LEFT OUTER JOIN [ProductAttribute] t3 ON t2.[ProductAttributeId] = t3.[Id] LEFT OUTER JOIN [ProductAttributeValue] t4 ON t3.[Id] = t4.[ProductAttributeId]  WHERE t1.[Id] IN ({variantIdFetcher})";

            var variant = Repository
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductAttributeValue>("Id", "ProductAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<ProductVariant, ProductVariantAttribute>())
                .Relate<ProductAttribute>((productVariant, attribute) =>
                {
                    var pva = productVariant.ProductVariantAttributes.FirstOrDefault(x => x.ProductAttributeId == attribute.Id);
                    if (pva != null)
                        pva.ProductAttribute = attribute;
                })
                .Relate<ProductAttributeValue>((productVariant, value) =>
                {
                    var pva = productVariant.ProductVariantAttributes.FirstOrDefault(x => x.ProductAttributeValueId == value.Id);
                    if (pva != null)
                    {
                        pva.ProductAttributeValue = value;
                    }
                })
                .QueryNested(sql)
                .FirstOrDefault();
            return variant;
        }

        public override ProductVariant Get(int id)
        {
            return GetQuery(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }

        public IList<ProductVariant> GetByProductId(int productId)
        {
            return GetQuery(x => x.ProductId == productId)
                .SelectNested()
                .ToList();
        }

        private IEntitySet<ProductVariant> GetQuery(Expression<Func<ProductVariant, bool>> where)
        {
            return Repository.Where(where)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttributeValue>("ProductAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<ProductVariant, ProductVariantAttribute>())
                .Relate<ProductAttributeValue>((variant, value) =>
                {
                    var productVariantAttribute =
                        variant.ProductVariantAttributes.FirstOrDefault(x => x.ProductAttributeValueId == value.Id);
                    if (productVariantAttribute != null)
                        productVariantAttribute.ProductAttributeValue = value;

                })
                .Relate<ProductAttribute>((variant, attribute) =>
                {
                    var productVariantAttribute =
                        variant.ProductVariantAttributes.FirstOrDefault(x => x.ProductAttributeId == attribute.Id);
                    if (productVariantAttribute != null && productVariantAttribute.ProductAttribute == null)
                        productVariantAttribute.ProductAttribute = attribute;

                })
                .Relate<AvailableAttribute>((variant, attribute) =>
                {
                    var productVariantAttribute =
                        variant.ProductVariantAttributes.FirstOrDefault(
                            x => x.ProductAttribute.AvailableAttribute == null &&
                                 x.ProductAttribute.AvailableAttributeId == attribute.Id);
                    if (productVariantAttribute != null)
                    {
                        productVariantAttribute.ProductAttribute.AvailableAttribute = attribute;
                        if (productVariantAttribute.ProductAttribute.Label.IsNullEmptyOrWhiteSpace())
                            productVariantAttribute.ProductAttribute.Label = attribute.Name;
                    }

                })
                .Relate<AvailableAttributeValue>((variant, attributeValue) =>
                {
                    var pav = variant.ProductVariantAttributes.Select(x => x.ProductAttributeValue)
                        .FirstOrDefault(x => x.AvailableAttributeValueId == attributeValue.Id);
                    if (pav != null && pav.AvailableAttributeValue == null)
                        pav.AvailableAttributeValue = attributeValue;
                });
        }
    }
}