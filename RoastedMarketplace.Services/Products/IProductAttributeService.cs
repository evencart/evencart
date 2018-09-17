using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductAttributeService : IFoundationEntityService<ProductAttribute>
    {
        void RemoveProductAttributeValue(int productAttributeValueId);

        void RemoveProductAttribute(int productAttributeId);

        void AddProductAttributeValue(ProductAttributeValue productAttributeValue);
    }
}