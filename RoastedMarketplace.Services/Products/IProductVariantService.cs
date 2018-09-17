using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductVariantService : IFoundationEntityService<ProductVariant>
    {
        void DeleteVariantsByProductAttributeValueId(int productAttributeValueId);

        ProductVariant AddVariant(Product product, IList<ProductVariantAttribute> variantAttributes);

        ProductVariant GetByAttributeValueIds(IList<int> productAttributeValueIds);

        IList<ProductVariant> GetByProductId(int productId);
    }
}