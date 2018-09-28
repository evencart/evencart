using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductVariantService : IFoundationEntityService<ProductVariant>
    {
        void DeleteVariantsByProductAttributeValueId(int productAttributeValueId, Transaction transaction = null);

        void DeleteVariantsByProductAttributeId(int productAttributeId, Transaction transaction = null);

        ProductVariant AddVariant(Product product, ProductVariant variant, Transaction transaction = null);

        ProductVariant GetByAttributeValueIds(IList<int> productAttributeValueIds);

        IList<ProductVariant> GetByProductId(int productId);
    }
}