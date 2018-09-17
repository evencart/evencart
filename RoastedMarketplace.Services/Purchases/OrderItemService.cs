using System.Linq;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Purchases
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