using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Orders;

namespace RoastedMarketplace.Factories.Orders
{
    public interface IOrderModelFactory : IModelFactory<Order, OrderModel>, IModelFactory<OrderItem, OrderItemModel>
    {
        
    }
}