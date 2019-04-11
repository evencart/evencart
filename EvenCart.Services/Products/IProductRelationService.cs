using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Products
{
    public interface IProductRelationService : IFoundationEntityService<ProductRelation>
    {
        IList<ProductRelation> GetByProductId(int productId, ProductRelationType? relationType, int page = 1, int count = 15);

        void RelateProducts(int productIds, IList<int> destinationProductIds, ProductRelationType relationType, bool isReciprocal);
    }
}