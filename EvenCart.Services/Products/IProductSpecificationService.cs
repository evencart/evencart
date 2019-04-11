using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IProductSpecificationService : IFoundationEntityService<ProductSpecification>
    {
        IList<ProductSpecification> GetByProductId(int productId, int? groupId = null);
    }
}