using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IProductSpecificationService : IFoundationEntityService<ProductSpecification>
    {
        IList<ProductSpecification> GetByProductId(int productId, int? groupId = null);
    }
}