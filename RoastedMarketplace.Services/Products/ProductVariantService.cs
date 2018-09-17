using System.Collections.Generic;
using System.Linq;
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

        public void DeleteVariantsByProductAttributeValueId(int productAttributeValueId)
        {
            //first get product variant attributes
            var productVariantAttributes = EntitySet<ProductVariantAttribute>
                .Where(x => x.ProductAttributeValueId == productAttributeValueId)
                .Select();

            //get all variants ids
            var productVariantIds = productVariantAttributes.Select(x => x.ProductVariantId).Distinct().ToList();

            //delete attributes
            EntitySet<ProductVariantAttribute>.Delete(x => productVariantIds.Contains(x.ProductVariantId));
            Delete(x => productVariantIds.Contains(x.Id));
        }

        public ProductVariant AddVariant(Product product, IList<ProductVariantAttribute> variantAttributes)
        {
            var productVariant = new ProductVariant()
            {
                ProductId = product.Id,
                TrackInventory = product.TrackInventory
            };
            Insert(productVariant);

            foreach (var variantAttribute in variantAttributes)
                variantAttribute.ProductVariantId = productVariant.Id;

            EntitySet<ProductVariantAttribute>.Insert(variantAttributes.ToArray());
            productVariant.ProductVariantAttributes = variantAttributes;

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

        public IList<ProductVariant> GetByProductId(int productId)
        {
            return Repository.Where(x => x.ProductId == productId)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<ProductVariant, ProductVariantAttribute>())
                .SelectNested()
                .ToList();
        }
    }
}