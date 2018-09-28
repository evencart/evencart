using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductAttributeService : IFoundationEntityService<ProductAttribute>
    {
        void RemoveProductAttributeValue(int productAttributeValueId);

        void RemoveProductAttribute(int productAttributeId);

        void AddProductAttributeValue(ProductAttributeValue productAttributeValue, Transaction transaction = null);

        IList<ProductAttribute> GetByProductId(int productId, bool onlyVariantSpecific = false);
    }
}