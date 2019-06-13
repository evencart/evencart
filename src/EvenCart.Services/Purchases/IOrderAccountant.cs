using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IOrderAccountant
    {
        IList<OrderFulfillment> GetAutoOrderFulfillments(Order order);

        IList<OrderFulfillment> SaveAutOrderFulfillments(Order order);

        void InsertCompleteOrder(Order order, Transaction transaction = null);

        Order CloneOrder(Order order);
    }
}