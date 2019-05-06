using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Orders;

namespace EvenCart.Factories.Orders
{
    public interface IOrderModelFactory : IModelFactory<Order, OrderModel>, IModelFactory<OrderItem, OrderItemModel>
    {
        
    }
}