using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IOrderAccountant
    {
        IList<OrderFulfillment> GetAutoOrderFulfillments(Order order);
    }
}