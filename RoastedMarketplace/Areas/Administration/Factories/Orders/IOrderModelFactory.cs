using RoastedMarketplace.Areas.Administration.Models.Orders;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Areas.Administration.Factories.Orders
{
    public interface IOrderModelFactory : IModelFactory<Order, OrderModel>, IModelFactory<OrderItem, OrderItemModel>
    {
        
    }
}