using System.Collections.Generic;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class WarehouseInventoryService : FoundationEntityService<WarehouseInventory>, IWarehouseInventoryService
    {
        public IEnumerable<WarehouseInventory> GetByProduct(int productId)
        {
            return Repository
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<WarehouseInventory, Warehouse>())
                .Relate<Address>((inventory, address) => { inventory.Warehouse.Address = address; })
                .Where(x => x.ProductId == productId)
                .SelectNested();
        }

        /*.Join<ProductVariant>("VariantId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", typeof(ProductVariantAttribute), JoinType.LeftOuter)
                .Join<ProductAttributeValue>("ProductAttributeValueId", "Id", typeof(ProductVariantAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", typeof(ProductAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", typeof(ProductAttributeValue),
                    joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<WarehouseInventory, ProductVariant>())
                .Relate<ProductVariantAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    if (variant != null)
                    {
                        attribute.ProductVariant = variant;
                        variant.ProductVariantAttributes =
                            variant.ProductVariantAttributes ?? new List<ProductVariantAttribute>();
                        if (!variant.ProductVariantAttributes.Contains(attribute))
                            variant.ProductVariantAttributes.Add(attribute);
                    }
                })
                .Relate<ProductAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute = attribute;
                        }
                    }
                })
                .Relate<ProductAttributeValue>((inventory, attributeValue) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue = attributeValue;
                        }
                    }
                })
                .Relate<AvailableAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttribute.AvailableAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute.AvailableAttribute = attribute;
                            if (variantAttribute.ProductAttribute.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttribute.Label = attribute.Name;
                        }
                    }
                })
                .Relate<AvailableAttributeValue>((inventory, attributeValue) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeValue.AvailableAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue.AvailableAttributeValue = attributeValue;
                            if (variantAttribute.ProductAttributeValue.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttributeValue.Label = attributeValue.Value;
                        }
                    }
                })*/
    }
}