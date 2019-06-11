using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IOrderItemService : IFoundationEntityService<OrderItem>
    {
        IEnumerable<OrderItem> GetWithProducts(IList<int> orderItemIds);
    }
}