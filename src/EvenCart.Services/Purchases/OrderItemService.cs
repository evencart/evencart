using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Purchases
{
    public class OrderItemService : FoundationEntityService<OrderItem>, IOrderItemService
    {
        public override OrderItem Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<Order>("OrderId", "Id")
                .Relate(RelationTypes.OneToOne<OrderItem, Order>())
                .SelectNested()
                .FirstOrDefault();
        }
    }
}