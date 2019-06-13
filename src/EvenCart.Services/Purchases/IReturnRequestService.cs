using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IReturnRequestService : IFoundationEntityService<ReturnRequest>
    {
        IEnumerable<ReturnRequest> GetWithOrderDetails(out int totalResults, IList<ReturnRequestStatus> status = null, DateTime? startDate = null, DateTime? endDate = null,
            int page = 1, int count = int.MaxValue);

        IEnumerable<ReturnRequest> GetOrderReturnRequests(int orderId);

    }
}