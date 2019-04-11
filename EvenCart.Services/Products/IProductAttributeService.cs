using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IProductAttributeService : IFoundationEntityService<ProductAttribute>
    {
        void RemoveProductAttributeValue(int productAttributeValueId);

        void RemoveProductAttribute(int productAttributeId);

        void AddProductAttributeValue(ProductAttributeValue productAttributeValue, Transaction transaction = null);

        IList<ProductAttribute> GetByProductId(int productId, bool onlyVariantSpecific = false);
    }
}