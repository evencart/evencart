using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
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