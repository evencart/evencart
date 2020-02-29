using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IOrderService : IFoundationEntityService<Order>
    {
        IEnumerable<Order> GetOrders(out int totalResults, string productName = null, int? userId = null, int? storeId = null, IList<int> orderIds = null, IList<int> productIds = null, IList<OrderStatus> orderStatus = null, IList<PaymentStatus> paymentStatus = null, IList<int> vendorIds = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = int.MaxValue);

        IEnumerable<Order> GetOrdersMinimal(out int totalResults, string productName = null, int? userId = null, int? storeId = null, IList<int> orderIds = null, IList<int> productIds = null, IList<OrderStatus> orderStatus = null, IList<PaymentStatus> paymentStatus = null, IList<int> vendorIds = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = int.MaxValue);

        Order GetByGuid(string guid);

        Dictionary<OrderStatus, int> GetOrderCountsByStatus(int? storeId = null);
    }
}