using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IOrderAccountant
    {
        IList<OrderFulfillment> GetAutoOrderFulfillments(Order order);

        IList<OrderFulfillment> SaveAutOrderFulfillments(Order order);

        void InsertCompleteOrder(Order order);

        void CancelOrder(Order order, string cancellationReason, bool finalize = false);

        Order CloneOrder(Order order);
    }
}