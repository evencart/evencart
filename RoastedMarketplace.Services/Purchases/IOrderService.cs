using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Purchases
{
    public interface IOrderService : IFoundationEntityService<Order>
    {
        IEnumerable<Order> GetOrders(out int totalResults, string productName = null, int? userId = null, IList<int> orderIds = null, IList<int> productIds = null, IList<OrderStatus> orderStatus = null, IList<PaymentStatus> paymentStatus = null, IList<int> vendorIds = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = int.MaxValue);

        Order GetByGuid(string guid);
    }
}