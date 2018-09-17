using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Purchases
{
    public interface IOrderService : IFoundationEntityService<Order>
    {
        IEnumerable<Order> GetOrders(int? userId = null, int[] productIds = null, OrderStatus? orderStatus = null,
            int[] vendorIds = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = int.MaxValue);

        Order GetByGuid(string guid);
    }
}