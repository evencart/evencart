using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductRelationService : IFoundationEntityService<ProductRelation>
    {
        IList<ProductRelation> GetByProductId(int productId, ProductRelationType? relationType, int page = 1, int count = 15);

        void RelateProducts(int productIds, IList<int> destinationProductIds, ProductRelationType relationType, bool isReciprocal);
    }
}